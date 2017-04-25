using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Web.API.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Web.Http;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Core;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using Ploeh.AutoFixture;
using vvarscNET.Model.RequestModels.Authentication;
using vvarscNET.Model.Result;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Security;
using System.Net;

namespace vvarscNET.Web.API.UnitTest.Controllers
{
    [TestFixture, Category("Web.API: AuthenticationController")]
    public class AuthenticationControllerTest
    {
        private Fixture _fixture;
        private IAuthenticationService _authenticationService;
        private AuthenticationController _authenticationController;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _authenticationService = A.Fake<IAuthenticationService>();

            // Creating user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, _fixture.Create<string>()),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Create<string>())
            };

            // Assigning user claims and principal 
            var genericIdentity = new GenericIdentity("");
            genericIdentity.AddClaims(claims);
            var genericPrincipal = new GenericPrincipal(genericIdentity, new string[] { _fixture.Create<string>() });

            // Configuring the controller
            _authenticationController = new AuthenticationController(_authenticationService);
            _authenticationController.User = genericPrincipal;
            _authenticationController.Request = new HttpRequestMessage();
            _authenticationController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region ProcessLogin
        [Test]
        public void ProcessLogin_ShouldSucceedWhenMemberHasNoValidAccessToken()
        {
            // ------ Arrange ------ 
            var authResponseModel = _fixture.Create<AuthenticateMember_QRM>();
            var authRequestModel = new AuthenticateMemberRequestModel
            {
                UserName = authResponseModel.UserName,
                Password = _fixture.Create<string>()
            };

            var createTokenResult = _fixture.Create<AccessToken>();
            
            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).Returns(authResponseModel);
            A.CallTo(() => _authenticationService.GetLatestMemberAccessToken(A<string>.Ignored, A<int>.Ignored)).Returns(null);
            A.CallTo(() => _authenticationService.GenerateAccessToken(A<string>.Ignored, A<int>.Ignored)).Returns(createTokenResult);

            // --------- Act -------- 
            var result = _authenticationController.ProcessLogin(authRequestModel) as OkNegotiatedContentResult<AccessToken>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(createTokenResult, result.Content);
            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _authenticationService.GetLatestMemberAccessToken(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _authenticationService.GenerateAccessToken(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void ProcessLogin_ShouldSucceedWhenMemberValidAccessToken()
        {
            // ------ Arrange ------ 
            var authResponseModel = _fixture.Create<AuthenticateMember_QRM>();
            var authRequestModel = new AuthenticateMemberRequestModel
            {
                UserName = authResponseModel.UserName,
                Password = _fixture.Create<string>()
            };

            var getTokenResult = _fixture.Create<AccessToken>();
            getTokenResult.member_pid = authResponseModel.MemberPID;
            getTokenResult.expiration = DateTime.Now.AddDays(1);
            getTokenResult.expiration_offline = DateTime.Now.AddDays(1);

            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).Returns(authResponseModel);
            A.CallTo(() => _authenticationService.GetLatestMemberAccessToken(A<string>.Ignored, A<int>.Ignored)).Returns(getTokenResult);

            // --------- Act -------- 
            var result = _authenticationController.ProcessLogin(authRequestModel) as OkNegotiatedContentResult<AccessToken>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(getTokenResult, result.Content);
            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _authenticationService.GetLatestMemberAccessToken(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void ProcessLogin_ShouldReturn401WhenMemberNotAuthorized()
        {
            // ------ Arrange ------
            var authRequestModel = new AuthenticateMemberRequestModel
            {
                UserName = _fixture.Create<string>(),
                Password = _fixture.Create<string>()
            };

            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).Returns(null);

            // --------- Act -------- 
            var result = _authenticationController.ProcessLogin(authRequestModel) as ResponseMessageResult;

            // ------- Assert -------
            Assert.IsInstanceOf<ResponseMessageResult>(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.Response.StatusCode);
            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void ProcessLogin_ShouldReturn500WhenAccessTokenCreationFails()
        {
            // ------ Arrange ------ 
            var authResponseModel = _fixture.Create<AuthenticateMember_QRM>();
            var authRequestModel = new AuthenticateMemberRequestModel
            {
                UserName = authResponseModel.UserName,
                Password = _fixture.Create<string>()
            };

            var createTokenResult = _fixture.Create<AccessToken>();

            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).Returns(authResponseModel);
            A.CallTo(() => _authenticationService.GetLatestMemberAccessToken(A<string>.Ignored, A<int>.Ignored)).Returns(null);
            A.CallTo(() => _authenticationService.GenerateAccessToken(A<string>.Ignored, A<int>.Ignored)).Returns(null);

            // --------- Act -------- 
            var result = _authenticationController.ProcessLogin(authRequestModel) as ResponseMessageResult;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
            A.CallTo(() => _authenticationService.AuthenticateMember(A<string>.Ignored, A<AuthenticateMemberRequestModel>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _authenticationService.GetLatestMemberAccessToken(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _authenticationService.GenerateAccessToken(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
        #endregion

        #region RenewToken
        [Test]
        public void RenewToken_ShouldReturn400WhenExistingTokenIsExpired()
        {
            // ------ Arrange ------ 
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();
            var existingTokenID = _fixture.Create<string>();
            var tokenRequestModel = new RenewTokenRequestModel
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AccessTokenID = existingTokenID
            };

            var tokenResponseModel = _fixture.Create<AccessToken>();
            tokenResponseModel.expiration = DateTime.Now.AddHours(-1);
            tokenResponseModel.expiration_offline = DateTime.Now.AddHours(-1);
            tokenResponseModel.member_pid = memberPID;
            
            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).Returns(tokenResponseModel);

            // --------- Act -------- 
            var result = _authenticationController.RenewToken(tokenRequestModel) as ResponseMessageResult;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.Response.StatusCode);
            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RenewToken_ShouldReturn400WhenExistingTokenDoesntMatchMemberPID()
        {
            // ------ Arrange ------ 
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();
            var existingTokenID = _fixture.Create<string>();
            var tokenRequestModel = new RenewTokenRequestModel
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AccessTokenID = existingTokenID
            };

            var tokenResponseModel = _fixture.Create<AccessToken>();
            tokenResponseModel.member_pid = _fixture.Create<string>();

            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).Returns(tokenResponseModel);

            // --------- Act -------- 
            var result = _authenticationController.RenewToken(tokenRequestModel) as ResponseMessageResult;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.Response.StatusCode);
            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RenewToken_ShouldReturn500WhenExistingTokenIsntFound()
        {
            // ------ Arrange ------ 
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();
            var existingTokenID = _fixture.Create<string>();
            var tokenRequestModel = new RenewTokenRequestModel
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AccessTokenID = existingTokenID
            };

            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).Returns(null);

            // --------- Act -------- 
            var result = _authenticationController.RenewToken(tokenRequestModel) as ResponseMessageResult;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RenewToken_ShouldSucceed()
        {
            // ------ Arrange ------ 
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();
            var existingTokenID = _fixture.Create<string>();
            var tokenRequestModel = new RenewTokenRequestModel
            {
                MemberPID = memberPID,
                ShellID = shellID,
                AccessTokenID = existingTokenID
            };

            var tokenResponseModel = _fixture.Create<AccessToken>();
            tokenResponseModel.member_pid = memberPID;
            tokenResponseModel.expiration = DateTime.Now.AddHours(1);
            tokenResponseModel.expiration_offline = DateTime.Now.AddHours(1);

            var newTokenResult = _fixture.Create<AccessToken>();
            newTokenResult.member_pid = memberPID;

            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).Returns(tokenResponseModel);
            A.CallTo(() => _authenticationService.RenewAccessToken(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).Returns(newTokenResult);

            // --------- Act -------- 
            var result = _authenticationController.RenewToken(tokenRequestModel) as OkNegotiatedContentResult<AccessToken>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(newTokenResult, result.Content);
            A.CallTo(() => _authenticationService.GetAccessTokenByID(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _authenticationService.RenewAccessToken(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion

        #region ProcessLogout
        [Test]
        public void ProcessLogout_ShouldReturn500WhenServiceFailsToExpireAccessToken()
        {
            // ------ Arrange ------ 
            var logoutRequestModel = new LogoutMemberRequestModel
            {
                MemberPID = _fixture.Create<string>(),
                ShellID = _fixture.Create<int>(),
                AccessTokenID = _fixture.Create<string>()
            };

            A.CallTo(() => _authenticationService.ExpireAccessToken(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).Returns(null);

            // --------- Act -------- 
            var result = _authenticationController.ProcessLogout(logoutRequestModel) as ResponseMessageResult;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.Response.StatusCode);
            A.CallTo(() => _authenticationService.ExpireAccessToken(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void ProcessLogout_ShouldSucceed()
        {
            string accessTokenID = _fixture.Create<string>();
            // ------ Arrange ------ 
            var logoutRequestModel = new LogoutMemberRequestModel
            {
                MemberPID = _fixture.Create<string>(),
                ShellID = _fixture.Create<int>(),
                AccessTokenID = accessTokenID
            };

            var cmdResult = new Result
            {
                Status = HttpStatusCode.OK,
                StatusDescription = "AccessToken Expired Successfully!",
                ItemIDs = new List<CompositeID>
                {
                    new CompositeID
                    {
                        IDType = "AccessTokenID",
                        IDValue = accessTokenID
                    }
                }
            };

            A.CallTo(() => _authenticationService.ExpireAccessToken(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).Returns(cmdResult);

            // --------- Act -------- 
            var result = _authenticationController.ProcessLogout(logoutRequestModel) as OkNegotiatedContentResult<Result>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.Content.Status);
            Assert.AreEqual(accessTokenID, result.Content.ItemIDs.FirstOrDefault().IDValue);
            A.CallTo(() => _authenticationService.ExpireAccessToken(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
        #endregion

    }
}
