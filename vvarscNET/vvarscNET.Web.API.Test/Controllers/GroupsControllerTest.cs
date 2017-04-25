using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Web.API.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Web.Http;
using vvarscNET.Model.ResponseModels.Groups;
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
    [TestFixture, Category("Web.API: GroupsController")]
    public class GroupsControllerTest
    {
        private Fixture _fixture;
        private IGroupsQueryService _groupsQueryService;
        private GroupsController _groupsController;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _groupsQueryService = A.Fake<IGroupsQueryService>();

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
            _groupsController = new GroupsController(_groupsQueryService);
            _groupsController.User = genericPrincipal;
            _groupsController.Request = new HttpRequestMessage();
            _groupsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region ListGroupsByShellAndGroupType
        [Test]
        public void ListGroupsByShellAndGroupType_ShouldSucceed()
        {
            // ------ Arrange ------ 
            var responseModel = _fixture.Create<ListGroupsByShellAndGroupType_QRM>();
            var responseModelList = new List<ListGroupsByShellAndGroupType_QRM>() { responseModel };
            A.CallTo(() => _groupsQueryService.ListGroupsByShellAndGroupType(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).Returns(responseModelList);

            // --------- Act -------- 
            var result = _groupsController.ListGroupsByShellAndGroupType(responseModel.ShellID, responseModel.GroupType) as OkNegotiatedContentResult<List<ListGroupsByShellAndGroupType_QRM>>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(responseModelList, result.Content);
            A.CallTo(() => _groupsQueryService.ListGroupsByShellAndGroupType(A<string>.Ignored, A<int>.Ignored, A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion
    }
}
