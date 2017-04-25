using System;
using System.Collections.Generic;
using NUnit.Framework;
using FakeItEasy;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Dispatchers;
using vvarscNET.Core.CommandModels.Members;
using vvarscNET.Model.Result;
using vvarscNET.Core.Data.CommandHandlers.Members;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.UnitTest
{
    [TestFixture, Category("Core: Dispatchers.CommandDispatcher")]
    public class CommandDispatcherTest
    {
        private ICommandDispatcher _commandDispatcher;
        private IContainer _container;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _container = A.Fake<IContainer>();
            _commandDispatcher = new CommandDispatcher(_container);
            _fixture = new Fixture();
        }

        [Test]
        public void CommandDispatcher_ShouldFailWhenContextIsNull()
        {
            var cmd = _fixture.Create<UpdateMember_C>();

            Assert.Throws<ArgumentNullException>(() => _commandDispatcher.Dispatch<UpdateMember_C>(null, cmd));
        }

        [Test]
        public void CommandDispatcher_ShouldFailWhenCommandIsNull()
        {
            var userContext = _fixture.Create<UserContext>();

            Assert.Throws<ArgumentNullException>(() => _commandDispatcher.Dispatch<UpdateMember_C>(userContext, null));
        }

        [Test]
        public void CommandDispatcher_ShouldSucceed()
        {
            var userContext = _fixture.Create<UserContext>();
            var cmd = _fixture.Create<UpdateMember_C>();
            var expectedResult = _fixture.Create<Result>();

            var commandHandler = A.Fake<ICommandHandler<UpdateMember_C>>();

            A.CallTo(() => commandHandler.Handle(A<UserContext>.Ignored, A<UpdateMember_C>.Ignored)).Returns(expectedResult);
            A.CallTo(() => _container.GetInstance<ICommandHandler<UpdateMember_C>>()).Returns(commandHandler);

            var result = _commandDispatcher.Dispatch<UpdateMember_C>(userContext, cmd);
            A.CallTo(() => commandHandler.Handle(A<UserContext>.Ignored, A<UpdateMember_C>.Ignored)).MustHaveHappened();

            Assert.AreSame(expectedResult, result);
        }
    }
}
