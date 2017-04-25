using System;
using FakeItEasy;
using log4net;
using NUnit.Framework;
using vvarscNET.Core.Extensions;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Logger;
using Ploeh.AutoFixture;

namespace vvarscNET.Core.UnitTest.Logger
{
    [TestFixture]
    [Category("Core: Logger.LogWriter")]
    public class LoggerWriterTests
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
        public void Log_ShouldCallLogInformationBasedOnLoggingEventType()
        {
            LoggerExtensions.Log(_logWriter, _fixture.Create<string>(), _fixture.Create<string>(), LoggingEventType.Information);
            A.CallTo(() => _log.Info(A<LogEntry>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Log_ShouldCallLogDebugBasedOnLoggingEventType()
        {
            LoggerExtensions.Log(_logWriter, _fixture.Create<string>(), _fixture.Create<string>(), LoggingEventType.Debug);
            A.CallTo(() => _log.Debug(A<LogEntry>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Log_ShouldCallLogWarningBasedOnLoggingEventType()
        {
            LoggerExtensions.Log(_logWriter, _fixture.Create<string>(), _fixture.Create<string>(), LoggingEventType.Warning);
            A.CallTo(() => _log.Warn(A<LogEntry>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Log_ShouldCallLogErrorBasedOnLoggingEventType()
        {
            LoggerExtensions.Log(_logWriter, _fixture.Create<string>(), _fixture.Create<string>(), LoggingEventType.Error);
            A.CallTo(() => _log.Error(A<LogEntry>.Ignored, A<Exception>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Log_ShouldCallLogFatalBasedOnLoggingEventType()
        {
            LoggerExtensions.Log(_logWriter, _fixture.Create<string>(), _fixture.Create<string>(), LoggingEventType.Fatal);
            A.CallTo(() => _log.Fatal(A<LogEntry>.Ignored, A<Exception>.Ignored)).MustHaveHappened();
        }
    }
}
