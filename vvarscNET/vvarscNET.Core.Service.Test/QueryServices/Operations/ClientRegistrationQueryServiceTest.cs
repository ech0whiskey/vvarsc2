using vvarscNET.Core.Interfaces;
using NUnit.Framework;
using FakeItEasy;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Service.QueryServices.Operations;
using vvarscNET.Model.ResponseModels.Operations;

namespace vvarscNET.Core.Service.UnitTest.QueryServices.Operations
{
    [TestFixture, Category("Core.Service: ClientRegistrationQueryService")]
    public class ClientRegistrationQueryServiceTest
    {
        private IQueryDispatcher _queryDispatcher;
        private IClientRegistrationQueryService _clientRegService;
        private Fixture _fixture; 

        [SetUp]
        public void BeforeEach()
        {
            _queryDispatcher = A.Fake<IQueryDispatcher>();
            _clientRegService = new ClientRegistrationQueryService(_queryDispatcher);
            _fixture = new Fixture();
        }

        [Test]
        public void GetClientRegistrationByAppName_ShouldFailWhenAppNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _clientRegService.GetClientRegistrationByAppName(_fixture.Create<string>(), null));
        }

        [Test]
        public void GetClientRegistrationByAppName_ShouldFailWhenAppNameIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => _clientRegService.GetClientRegistrationByAppName(_fixture.Create<string>(), string.Empty));
        }

        [Test]
        public void GetClientRegistrationByAppName_ShouldSucceed()
        {
            var expectedResult = new ClientRegistration
            {
                AppName = "testApp",
                IsActive = true,
                PrivateKey = "secret",
                ErrorReturnURL = "errorUrl",
                ReturnURL = "returnUrl"
            };

            var appName = "testApp";
            var accessToken = _fixture.Create<string>();

            var result = _clientRegService.GetClientRegistrationByAppName(accessToken, appName);

            Assert.AreEqual(expectedResult.AppName, result.AppName);
            Assert.AreEqual(expectedResult.IsActive, result.IsActive);
            Assert.AreEqual(expectedResult.PrivateKey, result.PrivateKey);
            Assert.AreEqual(expectedResult.ErrorReturnURL, result.ErrorReturnURL);
            Assert.AreEqual(expectedResult.ReturnURL, result.ReturnURL);
        }
    }
}
