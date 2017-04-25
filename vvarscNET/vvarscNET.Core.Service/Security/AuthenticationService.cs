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

        public AccessToken GenerateAccessToken(string memberPID, int shellID)
        {
            if (string.IsNullOrEmpty(memberPID))
                throw new ArgumentNullException(nameof(memberPID));

            var cmd = new CreateAccessToken_C
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AppID = 0,
                ApplicationPID = Globals.ApplicationPID,
                ExpiryDate = DateTime.Now.AddHours(1),
                OfflineExpiryDate = DateTime.Now.AddHours(1),
                TokenType = "Access",
                ParentToken = null,
                Version = "2.0"
            };

            AccessToken returnToken = null;
            var cmdResult = _commandDispatcher.Dispatch<CreateAccessToken_C>(new UserContext {
                MemberPID = "AdminCon",
                AccessTokenId = Globals.AuthHandlerToken
            }, cmd);

            if (cmdResult != null && cmdResult.ItemIDs != null && cmdResult.ItemIDs.Count > 0)
            {
                string newTokenID = cmdResult.ItemIDs.Where(a => a.IDType == "AccessTokenID").First().IDValue;
                string newPrivateKey = cmdResult.ItemIDs.Where(a => a.IDType == "PrivateKey").First().IDValue;

                //Get Member additional info To Populate into AccessToken
                var query = new GetMemberByPID_Q
                {
                    MemberPID = cmd.MemberPID
                };

                var memQueryResult = _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(newTokenID, query);

                if (memQueryResult != null)
                {
                    returnToken = new AccessToken();

                    returnToken.expiration = cmd.ExpiryDate;
                    returnToken.expiration_offline = cmd.OfflineExpiryDate;
                    returnToken.id = newTokenID;
                    returnToken.member_language = memQueryResult.Language;
                    returnToken.member_pid = cmd.MemberPID;
                    returnToken.member_private_key = newPrivateKey;
                    returnToken.member_region = memQueryResult.Region;
                    returnToken.member_timezone = memQueryResult.TimeZone;
                    returnToken.member_uri = "";
                    returnToken.token_type = "Access";
                    returnToken.application_pid = cmd.ApplicationPID;

                }
                else
                    throw new Exception("Unable To Retrive Member After Creating AccessToken");
            }
            else
            {
                throw new Exception("Error occurred in creating accessToken", new SystemException(cmdResult.StatusDescription));
            }

            return returnToken;
        }

        public AccessToken RenewAccessToken(string memberPID, int shellID, string parentTokenID)
        {
            if (string.IsNullOrEmpty(memberPID))
                throw new ArgumentNullException(nameof(memberPID));

            if (string.IsNullOrEmpty(parentTokenID))
                throw new ArgumentNullException(nameof(parentTokenID));

            var cmd = new CreateAccessToken_C
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AppID = 0,
                ApplicationPID = Globals.ApplicationPID,
                ExpiryDate = DateTime.Now.AddHours(1),
                OfflineExpiryDate = DateTime.Now.AddHours(1),
                TokenType = "Access",
                ParentToken = parentTokenID,
                Version = "2.0"
            };

            AccessToken returnToken = null;
            var cmdResult = _commandDispatcher.Dispatch<CreateAccessToken_C>(new UserContext
            {
                MemberPID = "AdminCon",
                AccessTokenId = Globals.AuthHandlerToken
            }, cmd);

            if (cmdResult != null && cmdResult.ItemIDs != null && cmdResult.ItemIDs.Count > 0)
            {
                string newTokenID = cmdResult.ItemIDs.Where(a => a.IDType == "AccessTokenID").First().IDValue;
                string newPrivateKey = cmdResult.ItemIDs.Where(a => a.IDType == "PrivateKey").First().IDValue;

                //Get Member additional info To Populate into AccessToken
                var query = new GetMemberByPID_Q
                {
                    MemberPID = cmd.MemberPID
                };

                var memQueryResult = _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(newTokenID, query);

                if (memQueryResult != null)
                {
                    returnToken = new AccessToken();

                    returnToken.expiration = cmd.ExpiryDate;
                    returnToken.expiration_offline = cmd.OfflineExpiryDate;
                    returnToken.id = newTokenID;
                    returnToken.member_language = memQueryResult.Language;
                    returnToken.member_pid = cmd.MemberPID;
                    returnToken.member_private_key = newPrivateKey;
                    returnToken.member_region = memQueryResult.Region;
                    returnToken.member_timezone = memQueryResult.TimeZone;
                    returnToken.member_uri = "";
                    returnToken.token_type = "Access";
                    returnToken.application_pid = cmd.ApplicationPID;

                }
                else
                    throw new Exception("Unable To Retrive Member After Creating AccessToken");
            }
            else
            {
                throw new Exception("Error occurred in creating accessToken", new SystemException(cmdResult.StatusDescription));
            }

            return returnToken;
        }

        public AccessToken GetLatestMemberAccessToken(string memberPID, int shellID)
        {
            if (string.IsNullOrEmpty(memberPID))
                throw new ArgumentNullException(nameof(memberPID));

            var query = new GetLatestMemberAccessToken_Q
            {
                MemberPID = memberPID,
                ShellID = shellID
            };

            var queryResult = _permQueryDispatcher.Dispatch<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>(query);

            if (queryResult != null)
            {
                return new AccessToken
                {
                    id = queryResult.AccessTokenID.ToString(),
                    expiration = queryResult.ExpiryDate,
                    expiration_offline = queryResult.OfflineExpiryDate,
                    member_language = queryResult.Language,
                    member_pid = queryResult.MemberPID,
                    member_private_key = queryResult.PrivateKey,
                    member_region = queryResult.Region,
                    member_timezone = queryResult.Timezone,
                    member_uri = null,
                    token_type = queryResult.TokenType,
                    application_pid = queryResult.ApplicationPID
                };
            }
            else
                return null;
        }

        public AccessToken GetAccessTokenByID(string accessTokenID)
        {
            if (string.IsNullOrEmpty(accessTokenID))
                throw new ArgumentNullException(nameof(accessTokenID));

            if (accessTokenID == Globals.AuthHandlerToken)
            {
                return new AccessToken
                {
                    id = accessTokenID,
                    expiration = DateTime.MaxValue,
                    expiration_offline = DateTime.MaxValue,
                    member_language = "en",
                    member_pid = "AdminCon",
                    member_private_key = Guid.NewGuid().ToString(),
                    member_region = "us",
                    member_timezone = "America/Los Angeles",
                    member_uri = null,
                    token_type = "Access",
                    application_pid = "AdminCon"
                };
            }

            var query = new GetAccessTokenByPID_Q
            {
                AccessTokenID = accessTokenID
            };

            var queryResult = _permQueryDispatcher.Dispatch<GetAccessTokenByPID_Q, GetAccessToken_QRM>(query);

            if (queryResult != null)
            {
                return new AccessToken
                {
                    id = queryResult.AccessTokenID.ToString(),
                    expiration = queryResult.ExpiryDate,
                    expiration_offline = queryResult.OfflineExpiryDate,
                    member_language = queryResult.Language,
                    member_pid = queryResult.MemberPID,
                    member_private_key = queryResult.PrivateKey,
                    member_region = queryResult.Region,
                    member_timezone = queryResult.Timezone,
                    member_uri = null,
                    token_type = queryResult.TokenType,
                    application_pid = queryResult.ApplicationPID
                };
            }
            else
                return null;
        }

        public Result ExpireAccessToken(string memberPID, int shellID, string accessTokenID)
        {
            if (string.IsNullOrEmpty(memberPID))
                throw new ArgumentNullException(nameof(memberPID));

            if (string.IsNullOrEmpty(accessTokenID))
                throw new ArgumentNullException(nameof(accessTokenID));

            var cmd = new ExpireAccessToken_C
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AccessTokenID = accessTokenID
            };

            var result = _commandDispatcher.Dispatch<ExpireAccessToken_C>(new UserContext
            {
                MemberPID = "AdminCon",
                AccessTokenId = Globals.AuthHandlerToken
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
