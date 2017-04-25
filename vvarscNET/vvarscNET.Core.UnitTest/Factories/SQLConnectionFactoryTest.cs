using NUnit.Framework;
using vvarscNET.Core.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.UnitTest.Factories
{
    [TestFixture]
    [Category("Core: Factories.SQLConnectionFactory")]
    class SQLConnectionFactoryTest
    {
        private Fixture _fixture;

        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Get_ShouldFailWhenConnectionStringIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SQLConnectionFactory(null));
        }

        [Test]
        public void Get_ShouldFailWhenConnectionStringIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new SQLConnectionFactory(string.Empty));
        }

        [Test]
        public void Get_ShouldFailWhenConnectionStringIsNotValid()
        {
            SQLConnectionFactory connFactory = new SQLConnectionFactory(_fixture.Create<string>());
            Assert.Throws<ArgumentException>(() => connFactory.GetConnection());
        }

        [Test]
        public void Get_ShouldCreateConnectionWhenConnectionStringIsValid()
        {
            SQLConnectionFactory connFactory = new SQLConnectionFactory(@"Data Source=localhost;initial catalog=Master;integrated security=true");
            System.Data.IDbConnection connection = connFactory.GetConnection();
            Assert.IsNotNull(connection);
        }
    }
}
