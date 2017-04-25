using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Web.API.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Web.Http;
using vvarscNET.Model.ResponseModels.Accounts;
using vvarscNET.Core;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using Ploeh.AutoFixture;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.API.UnitTest.Controllers
{
    [TestFixture, Category("Web.API: AccountsController")]
    public class AccountsControllerTest
    {
        private Fixture _fixture;
        private IAccountsQueryService _accountsQueryService;
        private AccountsController _accountsController;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _accountsQueryService = A.Fake<IAccountsQueryService>();

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
            _accountsController = new AccountsController(_accountsQueryService);
            _accountsController.User = genericPrincipal;
            _accountsController.Request = new HttpRequestMessage();
            _accountsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region ListActiveShells
        [Test]
        public void ListActiveShells_ShouldSucceed()
        {
            // ------ Arrange ------ 
            var responseModel = _fixture.Create<ListShells_QRM>();
            var responseModelList = new List<ListShells_QRM>() { responseModel };
            A.CallTo(() => _accountsQueryService.ListActiveShells(A<string>.Ignored)).Returns(responseModelList);

            // --------- Act -------- 
            var result = _accountsController.ListActiveShells() as OkNegotiatedContentResult<List<ListShells_QRM>>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(responseModelList, result.Content);
            A.CallTo(() => _accountsQueryService.ListActiveShells(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion
    }
}
