using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.QueryServices;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.QueryModels.Groups;
using vvarscNET.Model.ResponseModels.Groups;
using System;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Service.QueryServices.Groups;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.Service.UnitTest.QueryServices.Groups
{
    [TestFixture, Category("Core.Service: GroupsQueryService")]
    public class GroupsQueryServiceTest
    {
        private IGroupsQueryService _GroupsQueryService;
        private IQueryDispatcher _queryDispatcher;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _queryDispatcher = A.Fake<IQueryDispatcher>();
            _GroupsQueryService = new GroupsQueryService(_queryDispatcher);
            _fixture = new Fixture();
        }

        #region ListGroupsByShellAndGroupType

        [Test]
        public void ListGroupsByShellAndGroupType_ShouldSucceed()
        {
            var expectedResult = _fixture.Create<ListGroupsByShellAndGroupType_QRM>();
            var expectedResultList = new List<ListGroupsByShellAndGroupType_QRM>() { expectedResult };

            A.CallTo(() => _queryDispatcher.Dispatch<ListGroupsByShellAndGroupType_Q, List<ListGroupsByShellAndGroupType_QRM>>(A<string>.Ignored, A<ListGroupsByShellAndGroupType_Q>.Ignored)).Returns(expectedResultList);
            var result = _GroupsQueryService.ListGroupsByShellAndGroupType(_fixture.Create<string>(), _fixture.Create<int>(), _fixture.Create<string>());

            Assert.AreSame(expectedResultList, result);
            A.CallTo(() => _queryDispatcher.Dispatch<ListGroupsByShellAndGroupType_Q, List<ListGroupsByShellAndGroupType_QRM>>(A<string>.Ignored, A<ListGroupsByShellAndGroupType_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ListGroupsByShellAndGroupType_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;
            int shellID = _fixture.Create<int>();
            string groupType = _fixture.Create<string>();
            Assert.Throws<ArgumentNullException>(() => _GroupsQueryService.ListGroupsByShellAndGroupType(accessToken, shellID, groupType));
        }

        [Test]
        public void ListGroupsByShellAndGroupType_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;
            int shellID = _fixture.Create<int>();
            string groupType = _fixture.Create<string>();
            Assert.Throws<ArgumentNullException>(() => _GroupsQueryService.ListGroupsByShellAndGroupType(accessToken, shellID, groupType));
        }
        #endregion
    }
}
