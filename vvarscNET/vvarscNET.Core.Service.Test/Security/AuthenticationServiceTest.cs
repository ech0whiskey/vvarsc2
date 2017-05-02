using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Security;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Core.QueryModels.People;
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
using vvarscNET.Model.Objects.People;
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
        public void GenerateAccessToken_ShouldFailIfTokenCreationFailsInDB()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(new Result
            {
                Status = HttpStatusCode.InternalServerError,
                StatusDescription = "Inserted row count does not match submitted row count. Transaction rolled back.",
                ItemIDs = null
            });
            Assert.Throws<Exception>(() => _authenticationService.GenerateAccessToken(memberID, organizationID));
        }

        [Test]
        public void GenerateAccessToken_ShouldFailIfTokenCantBeRetrievedAfterInsert()
        {
            string accessToken = _fixture.Create<string>();
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<string>()
                {
                    accessToken
                }
            };

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).Returns(null);

            Assert.Throws<Exception>(() => _authenticationService.GenerateAccessToken(memberID, organizationID));
        }

        [Test]
        public void GenerateAccessToken_ShouldSucceed()
        {
            string accessToken = _fixture.Create<string>();
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<string>()
                {
                    accessToken
                }
            };

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.AccessToken = accessToken;
            tokenQueryResult.MemberID = memberID;
            tokenQueryResult.OrganizationID = organizationID;

            var expectedFinalResult = new AccessToken
            {
                ID = tokenQueryResult.ID.ToString(),
                MemberID = memberID.ToString(),
                AccessTokenValue = accessToken,
                ValidFrom = tokenQueryResult.ValidFrom,
                ValidTo = tokenQueryResult.ValidTo,
                OrganizationID = organizationID
            };

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).Returns(tokenQueryResult);

            var finalResult = _authenticationService.GenerateAccessToken(memberID, organizationID);

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).MustHaveHappened();
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).MustHaveHappened();

            //Can't Assert DateTime Values for expiry date becuase they are Created within the Command, and will always be different from the expected.
            Assert.AreEqual(expectedFinalResult.AccessTokenValue, finalResult.AccessTokenValue);
            Assert.AreEqual(expectedFinalResult.MemberID, finalResult.MemberID);
            Assert.AreEqual(expectedFinalResult.OrganizationID, finalResult.OrganizationID);
        }
        #endregion

        #region RenewAccessToken
        [Test]
        public void RenewAccessToken_ShouldFailWhenParentTokenIDIsNull()
        {
            var memberID = _fixture.Create<int>();
            var organizationID = _fixture.Create<int>();
            string parentTokenID = null;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.RenewAccessToken(memberID, organizationID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenParentTokenIDIsEmpty()
        {
            var memberID = _fixture.Create<int>();
            var organizationID = _fixture.Create<int>();
            string parentTokenID = string.Empty;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.RenewAccessToken(memberID, organizationID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenTokenCreationFailsInDB()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(new Result
            {
                Status = HttpStatusCode.InternalServerError,
                StatusDescription = "Inserted row count does not match submitted row count. Transaction rolled back.",
                ItemIDs = null
            });
            Assert.Throws<Exception>(() => _authenticationService.RenewAccessToken(memberID, organizationID, parentTokenID));
        }

        [Test]
        public void RenewAccessToken_ShouldFailWhenTokenCantBeRetrivedAfterInsert()
        {
            string accessToken = _fixture.Create<string>();
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<string>()
                {
                    accessToken
                }
            };

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).Returns(null);

            Assert.Throws<Exception>(() => _authenticationService.RenewAccessToken(memberID, organizationID, accessToken));
        }

        [Test]
        public void RenewAccessToken_ShouldSucceed()
        {
            string accessToken = _fixture.Create<string>();
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();
            string parentTokenID = _fixture.Create<string>();

            var firstCmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<string>()
                {
                    accessToken
                }
            };

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.AccessToken = accessToken;
            tokenQueryResult.MemberID = memberID;
            tokenQueryResult.OrganizationID = organizationID;

            var expectedFinalResult = new AccessToken
            {
                ID = tokenQueryResult.ID.ToString(),
                MemberID = memberID.ToString(),
                AccessTokenValue = accessToken,
                ValidFrom = tokenQueryResult.ValidFrom,
                ValidTo = tokenQueryResult.ValidTo,
                OrganizationID = organizationID
            };

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).Returns(firstCmdResult);
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).Returns(tokenQueryResult);

            var finalResult = _authenticationService.RenewAccessToken(memberID, organizationID, parentTokenID);

            A.CallTo(() => _commandDispatcher.Dispatch<CreateAccessToken_C>(A<UserContext>.Ignored, A<CreateAccessToken_C>.Ignored)).MustHaveHappened();
            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).MustHaveHappened();

            //Can't Assert DateTime Values for expiry date becuase they are Created within the Command, and will always be different from the expected.
            Assert.AreEqual(expectedFinalResult.AccessTokenValue, finalResult.AccessTokenValue);
            Assert.AreEqual(expectedFinalResult.MemberID, finalResult.MemberID);
            Assert.AreEqual(expectedFinalResult.OrganizationID, finalResult.OrganizationID);
        }
        #endregion

        #region GetLatestMemberAccessToken
        [Test]
        public void GetLatestMemberAccessToken_ShouldSucceed()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();

            var queryResult = _fixture.Create<GetAccessToken_QRM>();
            queryResult.MemberID = memberID;
            queryResult.OrganizationID = organizationID;

            var expectedFinalResult = new AccessToken
            {
                ID = queryResult.ID.ToString(),
                MemberID = memberID.ToString(),
                AccessTokenValue = queryResult.AccessToken,
                ValidFrom = queryResult.ValidFrom,
                ValidTo = queryResult.ValidTo,
                OrganizationID = organizationID
            };

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>(A<GetLatestMemberAccessToken_Q>.Ignored)).Returns(queryResult);
            var result = _authenticationService.GetLatestMemberAccessToken(memberID, organizationID);

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>(A<GetLatestMemberAccessToken_Q>.Ignored)).MustHaveHappened();

            Assert.AreEqual(expectedFinalResult.AccessTokenValue, result.AccessTokenValue);
            Assert.AreEqual(expectedFinalResult.MemberID, result.MemberID);
            Assert.AreEqual(expectedFinalResult.OrganizationID, result.OrganizationID);
        }

        #endregion

        #region GetAccessTokenByValue
        [Test]
        public void GetAccessTokenByValue_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GetAccessTokenByValue(accessToken));
        }

        [Test]
        public void GetAccessTokenByValue_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.GetAccessTokenByValue(accessToken));
        }

        [Test]
        public void GetAccessTokenByValue_ShouldSucceed()
        {
            var accessToken = _fixture.Create<string>();
            var queryResult = _fixture.Create<GetAccessToken_QRM>();
            queryResult.AccessToken = accessToken;

            var expectedFinalResult = new AccessToken
            {
                ID = queryResult.ID.ToString(),
                MemberID = queryResult.MemberID.ToString(),
                AccessTokenValue = accessToken,
                ValidFrom = queryResult.ValidFrom,
                ValidTo = queryResult.ValidTo,
                OrganizationID = queryResult.OrganizationID
            };

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).Returns(queryResult);
            var result = _authenticationService.GetAccessTokenByValue(accessToken);

            A.CallTo(() => _permQueryDispatcher.Dispatch<GetAccessTokenByValue_Q, GetAccessToken_QRM>(A<GetAccessTokenByValue_Q>.Ignored)).MustHaveHappened();

            Assert.AreEqual(expectedFinalResult.AccessTokenValue, result.AccessTokenValue);
            Assert.AreEqual(expectedFinalResult.MemberID, result.MemberID);
            Assert.AreEqual(expectedFinalResult.OrganizationID, result.OrganizationID);

        }
        #endregion

        #region ExpireAccessToken
        [Test]
        public void ExpireAccessToken_ShouldFailWhenAccessTokenIDIsNull()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();
            string accessTokenID = null;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.ExpireAccessToken(memberID, organizationID, accessTokenID));
        }

        [Test]
        public void ExpireAccessToken_ShouldFailWhenAccessTokenIDIsEmpty()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();
            string accessTokenID = string.Empty;

            Assert.Throws<ArgumentNullException>(() => _authenticationService.ExpireAccessToken(memberID, organizationID, accessTokenID));
        }

        [Test]
        public void ExpireAccessToken_ShouldFailWhenExpireAccessTokenFailsInDB()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();
            string accessTokenID = _fixture.Create<string>();

            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).Returns(new Result
            {
                Status = HttpStatusCode.InternalServerError,
                StatusDescription = "Updated row count does not match submitted row count. Transaction rolled back.",
                ItemIDs = null
            });

            Assert.Throws<Exception>(() => _authenticationService.ExpireAccessToken(memberID, organizationID, accessTokenID));

            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ExpireAccessToken_ShouldSucceed()
        {
            int memberID = _fixture.Create<int>();
            int organizationID = _fixture.Create<int>();
            string accessToken = _fixture.Create<string>();

            var expectedResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Created Successfully!",
                ItemIDs = new List<string> {
                    accessToken
                }
            };

            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).Returns(expectedResult);

            var result = _authenticationService.ExpireAccessToken(memberID, organizationID, accessToken);

            Assert.AreEqual(expectedResult, result);
            A.CallTo(() => _commandDispatcher.Dispatch<ExpireAccessToken_C>(A<UserContext>.Ignored, A<ExpireAccessToken_C>.Ignored)).MustHaveHappened();
        }

        #endregion
    }
}
