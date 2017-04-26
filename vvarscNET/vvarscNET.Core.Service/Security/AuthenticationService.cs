using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Model.RequestModels.Authentication;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Core.CommandModels.Authentication;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Model.ResponseModels.Members;
using System;
using System.Linq;
using System.Collections.Generic;
using vvarscNET.Model.Security;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPermissionQueryDispatcher _permQueryDispatcher;

        public AuthenticationService(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IPermissionQueryDispatcher permQueryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _permQueryDispatcher = permQueryDispatcher;
        }

        public AuthenticateMember_QRM AuthenticateMember(string accessToken, AuthenticateMemberRequestModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrEmpty(model.UserName))
                throw new ArgumentNullException(nameof(model.UserName));

            if (string.IsNullOrEmpty(model.Password))
                throw new ArgumentNullException(nameof(model.Password));

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var query = new AuthenticateMember_Q
            {
                UserName = model.UserName,
                Password = model.Password
            };

            return _permQueryDispatcher.Dispatch<AuthenticateMember_Q, AuthenticateMember_QRM>(query);
        }

        public AccessToken GenerateAccessToken(int memberID, int organizationID)
        {
            var cmd = new CreateAccessToken_C
            {
                MemberID = memberID,
                OrganizationID = organizationID,
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddHours(1),
                ParentAccessToken = string.Empty
            };

            AccessToken returnToken = null;
            var cmdResult = _commandDispatcher.Dispatch<CreateAccessToken_C>(new UserContext {
                MemberID = 0,
                AccessToken = Globals.AuthHandlerToken
            }, cmd);

            if (cmdResult != null && cmdResult.ItemIDs != null && cmdResult.ItemIDs.Count > 0)
            {
                string newTokenID = cmdResult.ItemIDs.First();

                var newTokenQuery = new GetAccessTokenByValue_Q
                {
                    AccessToken = newTokenID
                };

                //Get Full Token After Creation
                var returnTokenQRM = _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(newTokenQuery);

                if (returnTokenQRM != null)
                {
                    returnToken = new AccessToken
                    {
                        ID = returnTokenQRM.ID.ToString(),
                        MemberID = returnTokenQRM.MemberID.ToString(),
                        AccessTokenValue = returnTokenQRM.AccessToken,
                        ValidFrom = returnTokenQRM.ValidFrom,
                        ValidTo = returnTokenQRM.ValidTo,
                        OrganizationID = returnTokenQRM.OrganizationID
                    };
                }
                else
                {
                    throw new Exception("Unable to retrive AccessToken after Creation");
                }
            }
            else
            {
                throw new Exception("Error occurred in creating accessToken", new SystemException(cmdResult.StatusDescription));
            }

            return returnToken;
        }

        public AccessToken RenewAccessToken(int memberID, int organizationID, string parentToken)
        {
            if (string.IsNullOrEmpty(parentToken))
                throw new ArgumentNullException(nameof(parentToken));

            var cmd = new CreateAccessToken_C
            {
                MemberID = memberID,
                OrganizationID = organizationID,
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddHours(1),
                ParentAccessToken = parentToken
            };

            AccessToken returnToken = null;
            var cmdResult = _commandDispatcher.Dispatch<CreateAccessToken_C>(new UserContext
            {
                MemberID = 0,
                AccessToken = Globals.AuthHandlerToken
            }, cmd);

            if (cmdResult != null && cmdResult.ItemIDs != null && cmdResult.ItemIDs.Count > 0)
            {
                string newTokenID = cmdResult.ItemIDs.First();

                var newTokenQuery = new GetAccessTokenByValue_Q
                {
                    AccessToken = newTokenID
                };

                //Get Full Token After Creation
                var returnTokenQRM = _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(newTokenQuery);
                if (returnTokenQRM != null)
                {
                    returnToken = new AccessToken
                    {
                        ID = returnTokenQRM.ID.ToString(),
                        MemberID = returnTokenQRM.MemberID.ToString(),
                        AccessTokenValue = returnTokenQRM.AccessToken,
                        ValidFrom = returnTokenQRM.ValidFrom,
                        ValidTo = returnTokenQRM.ValidTo,
                        OrganizationID = returnTokenQRM.OrganizationID
                    };
                }
                else
                {
                    throw new Exception("Unable to retrive AccessToken after Creation");
                }
            }
            else
            {
                throw new Exception("Error occurred in creating accessToken", new SystemException(cmdResult.StatusDescription));
            }

            return returnToken;
        }

        public AccessToken GetLatestMemberAccessToken(int memberPID, int organizationID)
        {
            var query = new GetLatestMemberAccessToken_Q
            {
                MemberID = memberPID,
                OrganizationID = organizationID
            };

            var queryResult = _permQueryDispatcher.Dispatch<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>(query);

            if (queryResult != null)
            {
                return new AccessToken
                {
                    ID = queryResult.ID.ToString(),
                    MemberID = queryResult.MemberID.ToString(),
                    AccessTokenValue = queryResult.AccessToken,
                    ValidFrom = queryResult.ValidFrom,
                    ValidTo = queryResult.ValidTo,
                    OrganizationID = queryResult.OrganizationID
                };
            }
            else
                return null;
        }

        public AccessToken GetAccessTokenByValue(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            if (accessToken == Globals.AuthHandlerToken)
            {
                return new AccessToken
                {
                    ID = "0",
                    MemberID = "0",
                    AccessTokenValue = accessToken,
                    ValidFrom = DateTime.MinValue,
                    ValidTo = DateTime.MaxValue,
                    OrganizationID = 0
                };
            }

            var query = new GetAccessTokenByValue_Q
            {
                AccessToken = accessToken
            };

            var queryResult = _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(query);

            if (queryResult != null)
            {
                return new AccessToken
                {
                    ID = queryResult.ID.ToString(),
                    MemberID = queryResult.MemberID.ToString(),
                    AccessTokenValue = queryResult.AccessToken,
                    ValidFrom = queryResult.ValidFrom,
                    ValidTo = queryResult.ValidTo,
                    OrganizationID = queryResult.OrganizationID
                };
            }
            else
                return null;
        }

        public Result ExpireAccessToken(int memberID, int organizationID, string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var cmd = new ExpireAccessToken_C
            {
                MemberID = memberID,
                OrganizationID = organizationID,
                AccessToken = accessToken
            };

            var result = _commandDispatcher.Dispatch<ExpireAccessToken_C>(new UserContext
            {
                MemberID = 0,
                AccessToken = Globals.AuthHandlerToken
            }, cmd);

            if (result != null && result.ItemIDs != null && result.ItemIDs.Count > 0)
            {
                return result;
            }
            else
                throw new Exception("Error occurred in expiring accessToken", new SystemException(result.StatusDescription));
        }

    }
}
