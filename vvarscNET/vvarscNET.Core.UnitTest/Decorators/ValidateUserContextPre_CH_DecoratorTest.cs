using FakeItEasy;
using NUnit.Framework;
using vvarscNET.Core;
using vvarscNET.Core.Decorators;
using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Model.Result;
using Ploeh.AutoFixture;
using System.Net;
using vvarscNET.Core.Data.QueryHandlers.Authentication;
using vvarscNET.Core.Data.QueryHandlers.Members;

namespace vvarscNET.Core.UnitTest.Decorators
{
    [TestFixture]
    [Category("Core: Decorators.ValidateUserContext")]
    public class ValidateUserContextPre_CH_DecoratorTest
    {
        private Fixture _fixture;
        private IPermissionQueryHandler<GetMemberByAccessToken_Q, GetMemberByAccessToken_QRM> _getMemberByAccessToken_QH;
        private IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM> _getAccessTokenByPID_QH;

        [SetUp]
        public void BeforeEach()
        {
            _fixture = new Fixture();
            _getMemberByAccessToken_QH = A.Fake<IPermissionQueryHandler<GetMemberByAccessToken_Q, GetMemberByAccessToken_QRM>>();
            _getAccessTokenByPID_QH = A.Fake<IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM>>();
        }

        [Test]
        public void ValidateUserContext_ShouldFailWhenUserContextIsNull()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);

            Assert.Throws<ArgumentNullException>(() => decorator.Handle(null, cmd));
        }

        [Test]
        public void ValidateUserContext_ShouldFailWhenAccessTokenIsNull()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);

            var userContext = new UserContext();
            Assert.Throws<ArgumentNullException>(() => decorator.Handle(userContext, cmd));
        }

        [Test]
        public void ValidateUserContext_ShouldFailWhenCommandIsNull()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);

            var userContext = new UserContext { AccessTokenId = "ACCESS_TOKEN" };
            Assert.Throws<ArgumentNullException>(() => decorator.Handle(userContext, null));
        }

        [Test]
        public void ValidateUserContext_ShouldFailWhenAccessTokenNotFound()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(null);

            var userContext = new UserContext { AccessTokenId = "ACCESS_TOKEN" };
            var cmdResult = decorator.Handle(userContext, cmd);

            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(HttpStatusCode.NotFound, cmdResult.Status);
            Assert.AreEqual("Could not find AccessToken", cmdResult.StatusDescription);
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ValidateUserContext_ShouldFailWhenAccessTokenExpired()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.ExpiryDate = DateTime.Now.AddHours(-1);
            tokenQueryResult.OfflineExpiryDate = DateTime.Now.AddHours(-1);

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(tokenQueryResult);

            var userContext = new UserContext { AccessTokenId = "ACCESS_TOKEN" };
            var cmdResult = decorator.Handle(userContext, cmd);

            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(HttpStatusCode.Unauthorized, cmdResult.Status);
            Assert.AreEqual("AccessToken Expired", cmdResult.StatusDescription);
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ValidateUserContext_ShouldFailWhenPersonNotFound()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.ExpiryDate = DateTime.Now.AddHours(1);
            tokenQueryResult.OfflineExpiryDate = DateTime.Now.AddHours(1);
            tokenQueryResult.MemberPID = memberPID;
            tokenQueryResult.ShellID = shellID;

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(tokenQueryResult);
            A.CallTo(() => _getMemberByAccessToken_QH.Handle(A<GetMemberByAccessToken_Q>.Ignored)).Returns(null);

            var userContext = new UserContext { AccessTokenId = "ACCESS_TOKEN" };
            var cmdResult = decorator.Handle(userContext, cmd);

            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(HttpStatusCode.NotFound, cmdResult.Status);
            Assert.AreEqual("Could not find person by AccessToken", cmdResult.StatusDescription);
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
            A.CallTo(() => _getMemberByAccessToken_QH.Handle(A<GetMemberByAccessToken_Q>.Ignored)).MustHaveHappened();
        }
        
        [Test]
        public void ValidateUserContext_ShouldSucceedWithGlobalToken()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);

            A.CallTo((() => cmd.Handle(A<IUserContext>.Ignored, A<object>.Ignored))).Returns(
                new Result
                {
                    Status = HttpStatusCode.OK,
                    StatusDescription = "Success!"
                });

            var userContext = new UserContext
            {
                AccessTokenId = Globals.AuthHandlerToken
            };

            var cmdResult = decorator.Handle(userContext, cmd);
            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(HttpStatusCode.OK, cmdResult.Status);
        }

        [Test]
        public void ValidateUserContext_ShouldSucceedWithUserToken()
        {
            var cmd = A.Fake<ICommandHandler<object>>();

            var decorator = new ValidateUserContextPre_CH_Decorator<object>(cmd, _getMemberByAccessToken_QH, _getAccessTokenByPID_QH);
            var memberPID = _fixture.Create<string>();
            var shellID = _fixture.Create<int>();

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.ExpiryDate = DateTime.Now.AddHours(1);
            tokenQueryResult.OfflineExpiryDate = DateTime.Now.AddHours(1);
            tokenQueryResult.MemberPID = memberPID;
            tokenQueryResult.ShellID = shellID;

            var userQueryResult = _fixture.Create<GetMemberByAccessToken_QRM>();
            userQueryResult.MemberPID = memberPID;
            userQueryResult.ShellID = shellID;

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(tokenQueryResult);
            A.CallTo(() => _getMemberByAccessToken_QH.Handle(A<GetMemberByAccessToken_Q>.Ignored)).Returns(userQueryResult);
            A.CallTo((() => cmd.Handle(A<IUserContext>.Ignored, A<object>.Ignored))).Returns(
                new Result
                {
                    Status = HttpStatusCode.OK,
                    StatusDescription = "Success!"
                });

            var userContext = new UserContext { AccessTokenId = "ACCESS_TOKEN" };
            var cmdResult = decorator.Handle(userContext, cmd);

            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(HttpStatusCode.OK, cmdResult.Status);
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
            A.CallTo(() => _getMemberByAccessToken_QH.Handle(A<GetMemberByAccessToken_Q>.Ignored)).MustHaveHappened();
        }
    }
}
