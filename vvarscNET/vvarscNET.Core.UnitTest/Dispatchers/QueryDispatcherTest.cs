using System;
using System.Collections.Generic;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Dispatchers;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Model.Result;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Core.Data.QueryHandlers.Members;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.UnitTest
{
    [TestFixture, Category("Core: Dispatchers.QueryDispatcher")]
    public class QueryDispatcherTest
    {
        private IQueryDispatcher _queryDispatcher;
        private IContainer _container;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _container = A.Fake<IContainer>();
            _queryDispatcher = new QueryDispatcher(_container);
            _fixture = new Fixture();
        }

        [Test]
        public void QueryDispatcher_ShouldFailWhenAccessTokenIDIsNull()
        {
            var query = _fixture.Create<GetMemberByPID_Q>();

            Assert.Throws<ArgumentNullException>(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(null, query));
        }

        [Test]
        public void QueryDispatcher_ShouldFailWhenAccessTokenIDIsEmpty()
        {
            var query = _fixture.Create<GetMemberByPID_Q>();

            Assert.Throws<ArgumentNullException>(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(string.Empty, query));
        }

        [Test]
        public void QueryDispatcher_ShouldFailWhenCommandIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>("testAccessTokenID", null));
        }

        [Test]
        public void QueryDispatcher_ShouldSucceed()
        {
            var accessTokenId = _fixture.Create<string>();

            var query = _fixture.Create<GetMemberByPID_Q>();
            var expectedResult = _fixture.Create<GetMemberByPID_QRM>();

            var queryHandler = A.Fake<IQueryHandler<GetMemberByPID_Q, GetMemberByPID_QRM>>();

            A.CallTo(() => queryHandler.Handle(A<String>.Ignored, A<GetMemberByPID_Q>.Ignored)).Returns(expectedResult);
            A.CallTo(() => _container.GetInstance<IQueryHandler<GetMemberByPID_Q, GetMemberByPID_QRM>>()).Returns(queryHandler);

            var result = _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(accessTokenId, query);
            A.CallTo(() => queryHandler.Handle(A<string>.Ignored, A<GetMemberByPID_Q>.Ignored)).MustHaveHappened();

            Assert.AreSame(expectedResult, result);
        }
    }
}
