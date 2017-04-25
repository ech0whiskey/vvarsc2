using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Web.API.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Web.Http;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Core;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using Ploeh.AutoFixture;
using vvarscNET.Model.RequestModels.Members;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.API.UnitTest.Controllers
{
    [TestFixture, Category("Web.API: MembersController")]
    public class MembersControllerTest
    {
        private Fixture _fixture;
        private IMembersQueryService _memberQueryService;
        private IMemberCommandService _memberCommandService;
        private MembersController _membersController;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _memberQueryService = A.Fake<IMembersQueryService>();
            _memberCommandService = A.Fake<IMemberCommandService>();

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
            _membersController = new MembersController(_memberQueryService, _memberCommandService);
            _membersController.User = genericPrincipal;
            _membersController.Request = new HttpRequestMessage();
            _membersController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region GetMemberByPID
        [Test]
        public void GetMemberByPID_ShouldSucceed()
        {
            // ------ Arrange ------ 
            var responseModel = _fixture.Create<GetMemberByPID_QRM>();
            A.CallTo(() => _memberQueryService.GetMemberByPID(A<string>.Ignored, A<string>.Ignored)).Returns(responseModel);

            // --------- Act -------- 
            var result = _membersController.GetMemberByPID(responseModel.MemberPID) as OkNegotiatedContentResult<GetMemberByPID_QRM>;

            // ------- Assert -------
            Assert.IsNotNull(result);
            Assert.AreSame(responseModel, result.Content);
            A.CallTo(() => _memberQueryService.GetMemberByPID(A<string>.Ignored, A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion

        #region UpdateMember
        [Test]
        public void UpdateMember_ShouldUpdateMember()
        {
            // ------ Arrange ------ 
            var requestModel = _fixture.Create<UpdateMemberRequestModel>();
            var svcResult = _fixture.Create<Result>();

            A.CallTo(() => _memberCommandService.UpdateMember(A<UserContext>.Ignored, requestModel)).Returns(svcResult);

            // --------- Act -------- 
            var ctorlResult = _membersController.UpdateMember(requestModel) as OkNegotiatedContentResult<Result>;

            // ------- Assert -------
            Assert.IsNotNull(ctorlResult);
            Assert.AreSame(svcResult, ctorlResult.Content);
            A.CallTo(() => _memberCommandService.UpdateMember(A<UserContext>.Ignored, requestModel)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion
    }
}
