using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.QueryServices;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Model.ResponseModels.Members;
using System;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Service.QueryServices.Members;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.Service.UnitTest.QueryServices.Members
{
    [TestFixture, Category("Core.Service: MemberQueryService")]
    public class MembersQueryServiceTest
    {
        private IMembersQueryService _memberQueryService;
        private IQueryDispatcher _queryDispatcher;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _queryDispatcher = A.Fake<IQueryDispatcher>();
            _memberQueryService = new MembersQueryService(_queryDispatcher);
            _fixture = new Fixture();
        }

        #region GetMemberByPID
        [Test]
        public void GetMemberByPID_ShouldSucceed()
        {
            var expectedResult = _fixture.Create<GetMemberByPID_QRM>();

            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).Returns(expectedResult);
            var result = _memberQueryService.GetMemberByPID(_fixture.Create<string>(), expectedResult.MemberPID);

            Assert.AreSame(expectedResult, result);
            A.CallTo(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void GetMemberByPID_ShouldFailWhenMemberPIDIsNull()
        {
            string memberPID = null;
            Assert.Throws<ArgumentNullException>(() => _memberQueryService.GetMemberByPID(_fixture.Create<string>(), memberPID));
        }

        [Test]
        public void GetMemberByPID_ShouldFailWhenMemberPIDIsEmpty()
        {
            string memberPID = string.Empty;
            Assert.Throws<ArgumentNullException>(() => _memberQueryService.GetMemberByPID(_fixture.Create<string>(), memberPID));
        }

        [Test]
        public void GetMemberByPID_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;
            Assert.Throws<ArgumentNullException>(() => _memberQueryService.GetMemberByPID(accessToken, _fixture.Create<string>()));
        }

        [Test]
        public void GetMemberByPID_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;
            Assert.Throws<ArgumentNullException>(() => _memberQueryService.GetMemberByPID(accessToken, _fixture.Create<string>()));
        }
        #endregion
    }
}
