using vvarscNET.Core.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using FakeItEasy;
using vvarscNET.Core.CommandModels.Members;
using vvarscNET.Model.RequestModels.Members;
using vvarscNET.Model.Result;
using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.CommandServices.Members;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.Service.UnitTest.CommandServices.Members
{
    [TestFixture, Category("Core.Service: MemberCommandService")]
    public class MemberCommandServiceTest
    {
        private ICommandDispatcher _commandDispatcher;
        private IMemberCommandService _memberCommandService;
        private Fixture _fixture;

        [SetUp]
        public void BeforeEach()
        {
            _commandDispatcher = A.Fake<ICommandDispatcher>();
            _memberCommandService = new MemberCommandService(_commandDispatcher);
            _fixture = new Fixture();
        }

        #region UpdateMember
        [Test]
        public void UpdateMember_ShouldFailWhenCommandIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _memberCommandService.UpdateMember(A.Fake<UserContext>(), null));
        }

        [Test]
        public void UpdateMember_ShouldFailWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _memberCommandService.UpdateMember(null, A.Fake<UpdateMemberRequestModel>()));
        }

        [Test]
        public void UpdateMember_ShouldSucceed()
        {
            var userContext = _fixture.Create<UserContext>();
            var cmd = _fixture.Create<UpdateMemberRequestModel>();
            var expectedResult = _fixture.Create<Result>();

            A.CallTo(() => _commandDispatcher.Dispatch<UpdateMember_C>(A<UserContext>.Ignored, A<UpdateMember_C>.Ignored)).Returns(expectedResult);
            var result = _memberCommandService.UpdateMember(userContext, cmd);

            Assert.AreSame(expectedResult, result);
            A.CallTo(() => _commandDispatcher.Dispatch<UpdateMember_C>(A<UserContext>.Ignored, A<UpdateMember_C>.Ignored)).MustHaveHappened();
        }

        #endregion
    }
}
