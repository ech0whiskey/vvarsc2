using vvarscNET.Core.Service.Interfaces.QueryServices;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.Interfaces;
using Ploeh.AutoFixture;
using System.Security;
using System;
using vvarscNET.Model.Security;
using vvarscNET.Model.ResponseModels.Operations;
using vvarscNET.Core.Service.Security;

namespace vvarscNET.Core.Service.UnitTest.Security
{
    [TestFixture, Category("Core.Service: Security.JwtTokenService")]
    public class JwtTokenServiceTest
    {
        private IClientRegistrationQueryService _clientRegQueryService;
        private ITokenService _tokenService;
        private Fixture _fixture;

        const string ValidJwt = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJUb2tlbklEIjoiMTIzNDU2Nzg5MCIsIk1lbWJlcklEIjoiSm9obiBEb2UiLCJBcHAiOiJBZG1pbkNvbnNvbGUgQXBwIiwiZXhwIjoxNzI0MjgwMDg0LCJuYmYiOjE3MjQyNzAwODR9.L5sIZl2x5mpwZsYKk3T4u1m4DXXBcu7kixJnncBsN9LRcO7l97KisDx_3zMcFzCmS0AmpImMC6fJfDt8ol8Fig";
        const string InvalidJwtNoApp = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJUb2tlbklEIjoiMTIzNDU2Nzg5MCIsIk1lbWJlcklEIjoiSm9obiBEb2UiLCJBcHAiOiIiLCJleHAiOjE3MjQyODAwODQsIm5iZiI6MTcyNDI3MDA4NH0.6a0udMjsjBUR7Gjl5BhL2d6u4jbWuyOHAjIRziSgBg_Me6RquMEFC0DgvM3TAJBBlJS2ZpbhSozauoC1lJ7bPg";

        [SetUp]
        public void BeforeEach()
        {
            _clientRegQueryService = A.Fake<IClientRegistrationQueryService>();
            _fixture = new Fixture();
            _tokenService = new JwtTokenService(_clientRegQueryService);
        }

        [Test]
        public void EncodeJsonWebToken_ShouldFailWhenPrivateKeyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.EncodeJsonWebToken(GetJsonWebToken(), null); });
        }

        [Test]
        public void EncodeJsonWebToken_ShouldFailWhenPrivateKeyIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.EncodeJsonWebToken(GetJsonWebToken(), string.Empty); });
        }

        [Test]
        public void EncodeJsonWebToken_ShouldEncodeJsonWebToken()
        {
            var result = _tokenService.EncodeJsonWebToken(GetJsonWebToken(), "secret");
            Assert.AreEqual(ValidJwt, result);
        }

        [Test]
        public void GetJsonWebToken_ShouldFailWhenJWTIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.GetJsonWebToken(null); });
        }

        [Test]
        public void GetJsonWebToken_ShouldFailWhenJWTIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.GetJsonWebToken(string.Empty); });
        }

        [Test]
        public void GetJsonWebToken_ShouldSucceed()
        {
            var expectedResult = GetJsonWebToken();
            var jwt = _tokenService.GetJsonWebToken(ValidJwt);
            Assert.AreEqual(expectedResult.TokenID, jwt.TokenID);
            Assert.AreEqual(expectedResult.App, jwt.App);
            Assert.AreEqual(expectedResult.MemberID, jwt.MemberID);
            Assert.AreEqual(expectedResult.Exp, jwt.Exp);
            Assert.AreEqual(expectedResult.Nbf, jwt.Nbf);
        }

        [Test]
        public void ValidateTokenHash_ShouldFailWhenJwtIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.ValidateTokenHash(null); });
        }

        [Test]
        public void ValidateTokenHash_ShouldFailWhenJwtIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.ValidateTokenHash(string.Empty); });
        }

        [Test]
        public void ValidateTokenHash_ShouldFailWhenAppIsInvalid()
        {
            Assert.Throws<ArgumentNullException>(() => { _tokenService.ValidateTokenHash(InvalidJwtNoApp); });
        }

        [Test]
        public void ValidateTokenHash_ShouldFailWhenClientRegistrationAppIsNotFound()
        {
            A.CallTo(() => _clientRegQueryService.GetClientRegistrationByAppName(A<string>.Ignored, A<string>.Ignored)).Returns(null);
            Assert.Throws<SecurityException>(() => { _tokenService.ValidateTokenHash(ValidJwt); });
            A.CallTo(() => _clientRegQueryService.GetClientRegistrationByAppName(A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ValidateJokenHash_ShouldSucceed()
        {
            var fakedResult = new ClientRegistration
            {
                AppName = "someapp",
                IsActive = true,
                PrivateKey = "secret",
                ErrorReturnURL = "errorUrl",
                ReturnURL = "returnUrl"
            };

            A.CallTo(() => _clientRegQueryService.GetClientRegistrationByAppName(A<string>.Ignored, A<string>.Ignored)).Returns(fakedResult);
            var result = _tokenService.ValidateTokenHash(ValidJwt);
            Assert.IsTrue(result);
        }


        private JwtApiToken GetJsonWebToken()
        {
            return new JwtApiToken
            {
                TokenID = "1234567890",
                MemberID = "John Doe",
                App = "AdminConsole App",
                Exp = 1724280084,
                Nbf = 1724270084
            };
        }
    }
}
