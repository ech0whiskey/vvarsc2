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
    [Category("Core: Decorators.ValidateAccessToken")]
    public class ValidateAccessTokenPre_QH_DecoratorTest
    {
        private Fixture _fixture;
        private IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM> _getAccessTokenByPID_QH;

        [SetUp]
        public void BeforeEach()
        {
            _fixture = new Fixture();
            _getAccessTokenByPID_QH = A.Fake<IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM>>();
        }

        [Test]
        public void ValidateAccessToken_ShouldFailWhenAccessTokenIsNull()
        {
            string accessToken = null;
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>,string>(queryHandler, _getAccessTokenByPID_QH);

            Assert.Throws<ArgumentNullException>(() => decorator.Handle(accessToken, query));
        }

        [Test]
        public void ValidateAccessToken_ShouldFailWhenAccessTokenIsEmpty()
        {
            string accessToken = string.Empty;
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>, string>(queryHandler, _getAccessTokenByPID_QH);

            Assert.Throws<ArgumentNullException>(() => decorator.Handle(accessToken, query));
        }

        [Test]
        public void ValidateAccessToken_ShouldFailWhenQueryIsNull()
        {
            string accessToken = _fixture.Create<string>();
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>, string>(queryHandler, _getAccessTokenByPID_QH);

            Assert.Throws<ArgumentNullException>(() => decorator.Handle(accessToken, null));
        }

        [Test]
        public void ValidateAccessToken_ShouldFailWhenAccessTokenNotFound()
        {
            string accessToken = _fixture.Create<string>();
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>, string>(queryHandler, _getAccessTokenByPID_QH);

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(null);

            Assert.Throws<UnauthorizedAccessException>(() => decorator.Handle(accessToken, query));
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ValidateAccessToken_ShouldFailWhenAccessTokenExpired()
        {
            string accessToken = _fixture.Create<string>();
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>, string>(queryHandler, _getAccessTokenByPID_QH);

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.ExpiryDate = DateTime.Now.AddHours(-1);
            tokenQueryResult.OfflineExpiryDate = DateTime.Now.AddHours(-1);

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(tokenQueryResult);

            Assert.Throws<UnauthorizedAccessException>(() => decorator.Handle(accessToken, query));
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ValidateAccessToken_ShouldSucceedWithGlobalToken()
        {
            string accessToken = Globals.AuthHandlerToken;
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>, string>(queryHandler, _getAccessTokenByPID_QH);
            
            A.CallTo(() => queryHandler.Handle(accessToken, query)).Returns("success");

            var result = decorator.Handle(accessToken, query);
            
            Assert.IsNotNull(result);
            A.CallTo(() => queryHandler.Handle(accessToken, query)).MustHaveHappened();
        }

        [Test]
        public void ValidateAccessToken_ShouldSucceedWithUserToken()
        {
            string accessToken = _fixture.Create<string>();
            var query = new Fake<IQuery<string>>().FakedObject;
            var queryHandler = new Fake<IQueryHandler<IQuery<string>, string>>().FakedObject;

            var decorator = new ValidateAccessTokenPre_QH_Decorator<IQuery<string>, string>(queryHandler, _getAccessTokenByPID_QH);

            var tokenQueryResult = _fixture.Create<GetAccessToken_QRM>();
            tokenQueryResult.ExpiryDate = DateTime.Now.AddHours(1);
            tokenQueryResult.OfflineExpiryDate = DateTime.Now.AddHours(1);

            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).Returns(tokenQueryResult);
            A.CallTo(() => queryHandler.Handle(accessToken, query)).Returns("success");

            var result = decorator.Handle(accessToken, query);

            Assert.IsNotNull(result);
            A.CallTo(() => _getAccessTokenByPID_QH.Handle(A<GetAccessTokenByPID_Q>.Ignored)).MustHaveHappened();
            A.CallTo(() => queryHandler.Handle(accessToken, query)).MustHaveHappened();
        }
    }
}
