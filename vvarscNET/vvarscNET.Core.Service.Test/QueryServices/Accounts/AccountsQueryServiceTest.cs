using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.QueryServices;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.QueryModels.Accounts;
using vvarscNET.Model.ResponseModels.Accounts;
using System;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Service.QueryServices.Accounts;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.Service.UnitTest.QueryServices.Accounts
{
    [TestFixture, Category("Core.Service: AccountsQueryService")]
    public class AccountsQueryServiceTest
    {
        private IAccountsQueryService _accountsQueryService;
        private IQueryDispatcher _queryDispatcher;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _queryDispatcher = A.Fake<IQueryDispatcher>();
            _accountsQueryService = new AccountsQueryService(_queryDispatcher);
            _fixture = new Fixture();
        }

        #region ListActiveAccounts

        [Test]
        public void ListActiveShells_ShouldSucceed()
        {
            var expectedResult = _fixture.Create<ListShells_QRM>();
            var expectedResultList = new List<ListShells_QRM>() { expectedResult };

            A.CallTo(() => _queryDispatcher.Dispatch<ListActiveShells_Q, List<ListShells_QRM>>(A<string>.Ignored, A<ListActiveShells_Q>.Ignored)).Returns(expectedResultList);
            var result = _accountsQueryService.ListActiveShells(_fixture.Create<string>());

            Assert.AreSame(expectedResultList, result);
            A.CallTo(() => _queryDispatcher.Dispatch<ListActiveShells_Q, List<ListShells_QRM>>(A<string>.Ignored, A<ListActiveShells_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ListActiveShells_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;
            Assert.Throws<ArgumentNullException>(() => _accountsQueryService.ListActiveShells(accessToken));
        }

        [Test]
        public void ListActiveShells_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;
            Assert.Throws<ArgumentNullException>(() => _accountsQueryService.ListActiveShells(accessToken));
        }
        #endregion
    }
}
