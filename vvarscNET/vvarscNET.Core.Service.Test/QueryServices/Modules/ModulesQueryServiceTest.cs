using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.QueryServices;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.QueryModels.Modules;
using vvarscNET.Model.ResponseModels.Modules;
using System;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Service.QueryServices.Modules;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.Service.UnitTest.QueryServices.Modules
{
    [TestFixture, Category("Core.Service: ModulesQueryService")]
    public class ModulesQueryServiceTest
    {
        private IModulesQueryService _modulesQueryService;
        private IQueryDispatcher _queryDispatcher;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _queryDispatcher = A.Fake<IQueryDispatcher>();
            _modulesQueryService = new ModulesQueryService(_queryDispatcher);
            _fixture = new Fixture();
        }

        #region ListFeedModulesForShell
        [Test]
        public void ListFeedModulesForShell_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;
            Assert.Throws<ArgumentNullException>(() => _modulesQueryService.ListFeedModulesForShell(accessToken, _fixture.Create<int>()));
        }

        [Test]
        public void ListFeedModulesForShell_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;
            Assert.Throws<ArgumentNullException>(() => _modulesQueryService.ListFeedModulesForShell(accessToken, _fixture.Create<int>()));
        }

        [Test]
        public void ListFeedModulesForShell_ShouldSucceed()
        {
            var expectedResult = _fixture.Create<ListFeedModulesForShell_QRM>();
            var expectedResultList = new List<ListFeedModulesForShell_QRM>() { expectedResult };

            A.CallTo(() => _queryDispatcher.Dispatch<ListFeedModulesForShell_Q, List<ListFeedModulesForShell_QRM>>(A<string>.Ignored, A<ListFeedModulesForShell_Q>.Ignored)).Returns(expectedResultList);
            var result = _modulesQueryService.ListFeedModulesForShell(_fixture.Create<string>(), _fixture.Create<int>());

            Assert.AreSame(expectedResultList, result);
            A.CallTo(() => _queryDispatcher.Dispatch<ListFeedModulesForShell_Q, List<ListFeedModulesForShell_QRM>>(A<string>.Ignored, A<ListFeedModulesForShell_Q>.Ignored)).MustHaveHappened();
        }
        #endregion

        #region ListLibraryModulesForShell
        [Test]
        public void ListLibraryModulesForShell_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;
            Assert.Throws<ArgumentNullException>(() => _modulesQueryService.ListLibraryModulesForShell(accessToken, _fixture.Create<int>()));
        }

        [Test]
        public void ListLibraryModulesForShell_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;
            Assert.Throws<ArgumentNullException>(() => _modulesQueryService.ListLibraryModulesForShell(accessToken, _fixture.Create<int>()));
        }

        [Test]
        public void ListLibraryModulesForShell_ShouldSucceed()
        {
            var expectedResult = _fixture.Create<ListLibraryModulesForShell_QRM>();
            var expectedResultList = new List<ListLibraryModulesForShell_QRM>() { expectedResult };

            A.CallTo(() => _queryDispatcher.Dispatch<ListLibraryModulesForShell_Q, List<ListLibraryModulesForShell_QRM>>(A<string>.Ignored, A<ListLibraryModulesForShell_Q>.Ignored)).Returns(expectedResultList);
            var result = _modulesQueryService.ListLibraryModulesForShell(_fixture.Create<string>(), _fixture.Create<int>());

            Assert.AreSame(expectedResultList, result);
            A.CallTo(() => _queryDispatcher.Dispatch<ListLibraryModulesForShell_Q, List<ListLibraryModulesForShell_QRM>>(A<string>.Ignored, A<ListLibraryModulesForShell_Q>.Ignored)).MustHaveHappened();
        }
        #endregion
    }
}
