using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Security;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Core.CommandModels.Authentication;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Model.Result;
using System;
using vvarscNET.Core.Service.Interfaces;
using Ploeh.AutoFixture;
using vvarscNET.Model.RequestModels.Authentication;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Model.Security;

namespace vvarscNET.Core.Service.UnitTest.Security
{
    [TestFixture, Category("Core.Service: AuthenticationService")]
    public class AuthenticationServiceTest
    {
        private IAuthenticationService _authenticationService;
        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;
        private IPermissionQueryDispatcher _permQueryDispatcher;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _queryDispatcher = A.Fake<IQueryDispatcher>();
            _commandDispatcher = A.Fake<ICommandDispatcher>();
            _permQueryDispatcher = A.Fake<IPermissionQueryDispatcher>();
            _fixture = new Fixture();
            _authenticationService = new AuthenticationService(_queryDispatcher, _commandDispatcher, _permQueryDispatcher);
        }

        #region AuthenticateMember
        [Test]
        public void AuthenticateMember_ShouldFailWhenRequestModelIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _authenticationService.AuthenticateMember(_fixture.Create<string>(), null));
        }

        [Test]
        public void AuthenticateMember_ShouldFailWhenUserNameIsNull()
        {
            var requestModel = new AuthenticateMemberRequestModel
            {
                UserName = null,
                Password = _fixture.Create<string>()
            };
            Assert.Throws<ArgumentNullException>(() => _authenticationService.AuthenticateMember(_fixture.Create<string>(), requestModel));
        }

        [Test]
        public void AuthenticateMember_ShouldFailWhenUserNameIsEmpty()
        {
            var requestModel = new AuthenticateMemberRequestModel
            {
                UserName = string.Empty,
                Password = _fixture.Create<string>()
            };
            Assert.Throws<ArgumentNullException>(() => _authenticationService.AuthenticateMember(_fixture.Create<string>(), requestModel));
        }

        [Test]
        public void AuthenticateMember_ShouldFailWhenPasswordIsNull()
        {
            var requestModel = new AuthenticateMemberRequestModel
            {
                UserName = _fixture.Create<string>(),
                Password = null
            };
            Assert.Throws<ArgumentNullException>(() => _authenticationService.AuthenticateMember(_fixture.Create<string>(), requestModel));
        }

        [Test]
        public void AuthenticateMember_ShouldFailWhenPasswordIsEmpty()
        {
            var requestModel = new AuthenticateMemberRequestModel
            {
                UserName = _fixture.Create<string>(),
                Password = string.Empty
            };
            Assert.Throws<ArgumentNullException>(() => _authenticationService.AuthenticateMember(_fixture.Create<string>(), requestModel));
        }

        [Test]
        public void AuthenticateMember_ShouldSucceed()
        {
            var expectedResult = _fixture.Create<AuthenticateMember_QRM>();

            var requestModel = new AuthenticateMemberRequestModel
            {
                UserName = expectedResult.UserName,
                Password = _fixture.Create<string>()
            };

            A.CallTo(() => _permQueryDispatcher.Dispatch<AuthenticateMember_Q, AuthenticateMember_QRM>(A<AuthenticateMember_Q>.Ignored)).Returns(expectedResult);
            var result = _authenticationService.AuthenticateMember(_fixture.Create<string>(), requestModel);

            Assert.AreSame(expectedResult, result);
            A.CallTo(() => _permQueryDispatcher.Dispatch<AuthenticateMember_Q, AuthenticateMember_QRM>(A<AuthenticateMember_Q>.Ignored)).MustHaveHappened();
        }
        #endregion

        #region GenerateAccessToken
        [Test]
        public void GenerateAccessToken_ShouldFailWhenMemberPIDIsNull()
        {
            string memberPID = null;
            var shellID = _fixture.Create<int>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GenerateAccessToken(memberPID, shellID));
        }

        [Test]
        public void GenerateAccessToken_ShouldFailWhenMemberPIDIsEmpty()
        {
            var memberPID = string.Empty;
            var shellID = _fixture.Create<int>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GenerateAccessToken(memberPID, shellID));
        }

        [Test]
        public void GenerateAccessToken_ShouldFailIfTokenCreationFailsInDB()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(new Result {
                Status = HttpStatusCode.InternalServerError,
                StatusDescription = "Inserted row count does not match submitted row count. Transaction rolled back.",
                ItemIDs = null
            });
            Assert.Throws<Exception>(() => _authenticationService.GenerateAccessToken(memberPID, shellID));
        }

        [Test]
        public void GenerateAccessToken_ShouldFailIfMemberCantBeRetrived()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<CompositeID> { new CompositeID
                    {
                        IDType = "AccessTokenID",
                        IDValue = _fixture.Create<string>()
                    },
                    new CompositeID
                    {
                        IDType = "PrivateKey",
                        IDValue = _fixture.Create<string>()
                    }
                }
            };

            string newTokenID = firstCmdResult.ItemIDs.Where(a => a.IDType == "AccessTokenID").First().IDValue;

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).Returns(null);

            Assert.Throws<Exception>(() => _authenticationService.GenerateAccessToken(memberPID, shellID));
        }
        
        [Test]
        public void GenerateAccessToken_ShouldSucceed()
        {
            string accessTokenID = _fixture.Create<string>();
            string privateKey = _fixture.Create<string>();
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<CompositeID> {
                    new CompositeID
                    {
                        IDType = "AccessTokenID",
                        IDValue = accessTokenID
                    },
                    new CompositeID
                    {
                        IDType = "PrivateKey",
                        IDValue = privateKey
                    }
                }
            };

            var memQueryResult = _fixture.Create<GetMemberByPID_QRM>();
            memQueryResult.MemberPID = memberPID;

            var expectedFinalResult = new AccessToken
            {
                id = accessTokenID,
                member_private_key = privateKey,
                expiration = _fixture.Create<DateTime>(),
                expiration_offline = _fixture.Create<DateTime>(),
                member_language = memQueryResult.Language,
                member_region = memQueryResult.Region,
                member_timezone = memQueryResult.TimeZone,
                member_pid = memQueryResult.MemberPID,
                member_uri = null,
                token_type = "Access",
                application_pid = Globals.ApplicationPID
            };

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).Returns(memQueryResult);

            var finalResult = _authenticationService.GenerateAccessToken(memberPID, shellID);

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).MustHaveHappened();
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).MustHaveHappened();

            //Can't Assert DateTime Values for expiry date becuase they are Created within the Command, and will always be different from the expected.
            Assert.AreEqual(expectedFinalResult.id, finalResult.id);
            Assert.AreEqual(expectedFinalResult.member_private_key, finalResult.member_private_key);
            Assert.AreEqual(expectedFinalResult.member_language, finalResult.member_language);
            Assert.AreEqual(expectedFinalResult.member_region, finalResult.member_region);
            Assert.AreEqual(expectedFinalResult.member_timezone, finalResult.member_timezone);
            Assert.AreEqual(expectedFinalResult.member_pid, finalResult.member_pid);
            Assert.AreEqual(expectedFinalResult.token_type, finalResult.token_type);
        }
        #endregion

        #region RenewAccessToken
        [Test]
        public void RenewAccessToken_ShouldFailWhenMemberPIDIsNull()
        {
            string memberPID = null;
            var shellID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenMemberPIDIsEmpty()
        {
            var memberPID = string.Empty;
            var shellID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenParentTokenIDIsNull()
        {
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();
            string parentTokenID = null;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenParentTokenIDIsEmpty()
        {
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();
            string parentTokenID = string.Empty;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenTokenCreationFailsInDB()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(new Result
            {
                Status = HttpStatusCode.InternalServerError,
                StatusDescription = "Inserted row count does not match submitted row count. Transaction rolled back.",
                ItemIDs = null
            });
            Assert.Throws<Exception>(() => _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenMemberCantBeRetrived()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<CompositeID> { new CompositeID
                    {
                        IDType = "AccessTokenID",
                        IDValue = _fixture.Create<string>()
                    },
                    new CompositeID
                    {
                        IDType = "PrivateKey",
                        IDValue = _fixture.Create<string>()
                    }
                }
            };

            string newTokenID = firstCmdResult.ItemIDs.Where(a => a.IDType == "AccessTokenID").First().IDValue;

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).Returns(null);

            Assert.Throws<Exception>(() => _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldSucceed()
        {
            string accessTokenID = _fixture.Create<string>();
            string privateKey = _fixture.Create<string>();
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<CompositeID> {
                    new CompositeID
                    {
                        IDType = "AccessTokenID",
                        IDValue = accessTokenID
                    },
                    new CompositeID
                    {
                        IDType = "PrivateKey",
                        IDValue = privateKey
                    }
                }
            };

            var memQueryResult = _fixture.Create<GetMemberByPID_QRM>();
            memQueryResult.MemberPID = memberPID;

            var expectedFinalResult = new AccessToken
            {
                id = accessTokenID,
                member_private_key = privateKey,
                expiration = _fixture.Create<DateTime>(),
                expiration_offline = _fixture.Create<DateTime>(),
                member_language = memQueryResult.Language,
                member_region = memQueryResult.Region,
                member_timezone = memQueryResult.TimeZone,
                member_pid = memQueryResult.MemberPID,
                member_uri = null,
                token_type = "Access",
                application_pid = Globals.ApplicationPID
            };

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).Returns(memQueryResult);

            var finalResult = _authenticationService.RenewAccessToken(memberPID, shellID, parentTokenID);

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).MustHaveHappened();
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).MustHaveHappened();

            //Can't Assert DateTime Values for expiry date becuase they are Created within the Command, and will always be different from the expected.
            Assert.AreEqual(expectedFinalResult.id, finalResult.id);
            Assert.AreEqual(expectedFinalResult.member_private_key, finalResult.member_private_key);
            Assert.AreEqual(expectedFinalResult.member_language, finalResult.member_language);
            Assert.AreEqual(expectedFinalResult.member_region, finalResult.member_region);
            Assert.AreEqual(expectedFinalResult.member_timezone, finalResult.member_timezone);
            Assert.AreEqual(expectedFinalResult.member_pid, finalResult.member_pid);
            Assert.AreEqual(expectedFinalResult.token_type, finalResult.token_type);
        }
        #endregion

        #region GetLatestMemberAccessToken
        [Test]
        public void GetLatestMemberAccessToken_ShouldFailWhenMemberPIDIsNull()
        {
            string memberPID = null;
            int shellID = _fixture.Create<int>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GetLatestMemberAccessToken(memberPID, shellID));
        }

        [Test]
        public void GetLatestMemberAccessToken_ShouldFailWhenMemberPIDIsEmpty()
        {
            string memberPID = string.Empty;
            int shellID = _fixture.Create<int>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GetLatestMemberAccessToken(memberPID, shellID));
        }

        [Test]
        public void GetLatestMemberAccessToken_ShouldSucceed()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();

            var queryResult = _fixture.Create<GetAccessToken_QRM>();

            var expectedFinalResult = new AccessToken
            {
                id = queryResult.AccessTokenID.ToString(),
                member_private_key = queryResult.PrivateKey,
                member_language = queryResult.Language,
                member_region = queryResult.Region,
                member_timezone = queryResult.Timezone,
                member_pid = queryResult.MemberPID,
                token_type = queryResult.TokenType,
                application_pid = queryResult.ApplicationPID
            };

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>(A<GetLatestMemberAccessToken_Q>.Ignored)).Returns(queryResult);
            var result = _authenticationService.GetLatestMemberAccessToken(memberPID, shellID);
            
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>(A<GetLatestMemberAccessToken_Q>.Ignored)).MustHaveHappened();

            Assert.AreEqual(expectedFinalResult.id, result.id);
            Assert.AreEqual(expectedFinalResult.member_language, result.member_language);
            Assert.AreEqual(expectedFinalResult.member_private_key, result.member_private_key);
            Assert.AreEqual(expectedFinalResult.member_region, result.member_region);
            Assert.AreEqual(expectedFinalResult.member_timezone, result.member_timezone);
            Assert.AreEqual(expectedFinalResult.member_pid, result.member_pid);
            Assert.AreEqual(expectedFinalResult.token_type, result.token_type);
        }

        #endregion

        #region GetAccessTokenByPID
        [Test]
        public void GetAccessTokenByPID_ShouldFailWhenAccessTokenIDIsNull()
        {
            string accessTokenID = null;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GetAccessTokenByID(accessTokenID));
        }

        [Test]
        public void GetAccessTokenByPID_ShouldFailWhenAccessTokenIDIsEmpty()
        {
            string accessTokenID = string.Empty;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GetAccessTokenByID(accessTokenID));
        }

        [Test]
        public void GetAccessTokenByPID_ShouldSucceed()
        {
            var accessTokenID = _fixture.Create<string>();
            var queryResult = _fixture.Create<GetAccessToken_QRM>();

            var expectedFinalResult = new AccessToken
            {
                id = queryResult.AccessTokenID.ToString(),
                member_private_key = queryResult.PrivateKey,
                member_language = queryResult.Language,
                member_region = queryResult.Region,
                member_timezone = queryResult.Timezone,
                member_pid = queryResult.MemberPID,
                token_type = queryResult.TokenType,
                application_pid = queryResult.ApplicationPID
            };

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByPID_Q,GetAccessToken_QRM>(A<GetAccessTokenByPID_Q>.Ignored)).Returns(queryResult);
            var result = _authenticationService.GetAccessTokenByID(accessTokenID);

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByPID_Q, GetAccessToken_QRM>(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();

            Assert.AreEqual(expectedFinalResult.id, result.id);
            Assert.AreEqual(expectedFinalResult.member_language, result.member_language);
            Assert.AreEqual(expectedFinalResult.member_private_key, result.member_private_key);
            Assert.AreEqual(expectedFinalResult.member_region, result.member_region);
            Assert.AreEqual(expectedFinalResult.member_timezone, result.member_timezone);
            Assert.AreEqual(expectedFinalResult.member_pid, result.member_pid);
            Assert.AreEqual(expectedFinalResult.token_type, result.token_type);

        }
        #endregion

        #region ExpireAccessToken
        [Test]
        public void ExpireAccessToken_ShouldFailWhenMemberPIDIsNull()
        {
            string memberPID = null;
            int shellID = _fixture.Create<int>();
            string accessTokenID = _fixture.Create<string>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.ExpireAccessToken(memberPID, shellID, accessTokenID));
        }

        [Test]
        public void ExpireAccessToken_ShouldFailWhenMemberPIDIsEmpty()
        {
            string memberPID = string.Empty;
            int shellID = _fixture.Create<int>();
            string accessTokenID = _fixture.Create<string>();

            Assert.Throws<ArgumentNullException>(() => _authenticationService.ExpireAccessToken(memberPID, shellID, accessTokenID));
        }

        [Test]
        public void ExpireAccessToken_ShouldFailWhenAccessTokenIDIsNull()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string accessTokenID = null;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.ExpireAccessToken(memberPID, shellID, accessTokenID));
        }

        [Test]
        public void ExpireAccessToken_ShouldFailWhenAccessTokenIDIsEmpty()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string accessTokenID = string.Empty;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.ExpireAccessToken(memberPID, shellID, accessTokenID));
        }

        [Test]
        public void ExpireAccessToken_ShouldFailWhenExpireAccessTokenFailsInDB()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string accessTokenID = _fixture.Create<string>();

            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).Returns(new Result
            {
                Status = HttpStatusCode.InternalServerError,
                StatusDescription = "Updated row count does not match submitted row count. Transaction rolled back.",
                ItemIDs = null
            });

            Assert.Throws<Exception>(() => _authenticationService.ExpireAccessToken(memberPID, shellID, accessTokenID));

            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ExpireAccessToken_ShouldSucceed()
        {
            string memberPID = _fixture.Create<string>();
            int shellID = _fixture.Create<int>();
            string accessTokenID = _fixture.Create<string>();

            var expectedResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<CompositeID> {
                    new CompositeID
                    {
                        IDType = "AccessTokenID",
                        IDValue = accessTokenID
                    }
                }
            };

            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).Returns(expectedResult);

            var result = _authenticationService.ExpireAccessToken(memberPID, shellID, accessTokenID);

            Assert.AreEqual(expectedResult, result);
            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).MustHaveHappened();
        }

        #endregion
    }
}
