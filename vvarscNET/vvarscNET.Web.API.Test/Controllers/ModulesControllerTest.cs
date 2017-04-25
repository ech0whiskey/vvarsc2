using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Web.API.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Web.Http;
using vvarscNET.Model.ResponseModels.Modules;
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
    [TestFixture, Category("Web.API: ModulesController")]
    public class ModulesControllerTest
    {
        private Fixture _fixture;
        private IModulesQueryService _modulesQueryService;
        private ModulesController _modulesController;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _modulesQueryService = A.Fake<IModulesQueryService>();

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
            _modulesController = new ModulesController(_modulesQueryService);
            _modulesController.User = genericPrincipal;
            _modulesController.Request = new HttpRequestMessage();
            _modulesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region ListFeedModulesForShell
        [Test]
        public void ListFeedModulesForShell_ShouldSucceed()
        {
            // ------ Arrange ------ 
            var responseModel = _fixture.Create<ListFeedModulesForShell_QRM>();
            var responseModelList = new List<ListFeedModulesForShell_QRM>() { responseModel };
            A.CallTo(() => _modulesQueryService.ListFeedModulesForShell(A<string>.Ignored, A<int>.Ignored)).Returns(responseModelList);

            // --------- Act -------- 
            var result = _modulesController.ListFeedModulesForShell(responseModel.ShellID) as OkNegotiatedContentResult<List<ListFeedModulesForShell_QRM>>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(responseModelList, result.Content);
            A.CallTo(() => _modulesQueryService.ListFeedModulesForShell(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion

        #region ListLibraryModulesForShell
        [Test]
        public void ListLibraryModulesForShell_ShouldSucceed()
        {
            // ------ Arrange ------ 
            var responseModel = _fixture.Create<ListLibraryModulesForShell_QRM>();
            var responseModelList = new List<ListLibraryModulesForShell_QRM>() { responseModel };
            A.CallTo(() => _modulesQueryService.ListLibraryModulesForShell(A<string>.Ignored, A<int>.Ignored)).Returns(responseModelList);

            // --------- Act -------- 
            var result = _modulesController.ListLibraryModulesForShell(responseModel.ShellID) as OkNegotiatedContentResult<List<ListLibraryModulesForShell_QRM>>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(responseModelList, result.Content);
            A.CallTo(() => _modulesQueryService.ListLibraryModulesForShell(A<string>.Ignored, A<int>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion
    }
}
