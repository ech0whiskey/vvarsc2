using System;
using FakeItEasy;
using log4net;
using NUnit.Framework;
using vvarscNET.Core.Extensions;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Logger;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.UnitTest.Extensions
{
    [TestFixture]
    [Category("Core: Extensions.LoggerExtensions")]
    public class LoggerExtensionsTests
    {
        private ILog _log;
        private ILogWriter _logWriter;
        private Fixture _fixture;

        [OneTimeSetUp]
        public void Setup()
        {
            _log = A.Fake<ILog>();
            _logWriter = new LogWriter(_log);
            _fixture = new Fixture();
        }

        [Test]
        public void Log_ShouldCallLogDebugBasedOnLoggingEventType()
        {
            LoggerExtensions.Log(_logWriter, _fixture.Create<string>(), _fixture.Create<string>(), LoggingEventType.Debug);
            A.CallTo(() => _log.Debug(A<LogEntry>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void LogInformation_ShouldCallLogInformation()
        {
            LoggerExtensions.LogInformation(_logWriter, _fixture.Create<string>(), _fixture.Create<string>());
            A.CallTo(() => _log.Info(A<LogEntry>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void LogError_ShouldCallLogError()
        {
            LoggerExtensions.LogError(_logWriter, _fixture.Create<string>(), new Exception(_fixture.Create<string>()));
            A.CallTo(() => _log.Error(A<LogEntry>.Ignored, A<Exception>.Ignored)).MustHaveHappened();
        }
    }
}
