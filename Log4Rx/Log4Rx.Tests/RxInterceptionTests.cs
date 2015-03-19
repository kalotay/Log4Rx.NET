using System;
using Log4Rx.Tests.RxInterception;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace Log4Rx.Tests
{
	[TestFixture(typeof(FatalLogLevel))]
	[TestFixture(typeof(ErrorLogLevel))]
	[TestFixture(typeof(WarnLogLevel))]
	[TestFixture(typeof(InfoLogLevel))]
	[TestFixture(typeof(DebugLogLevel))]
	public class RxInterceptionTests<T> where T: ILogLevel, new()
	{
		private readonly IObservable<LoggingEvent> _observable;
		private readonly ForwardingAppender _forwardingAppender;
		private readonly ILog _log;
		private readonly T _logLevel;

		private TestableAppender _testableAppender;

		public RxInterceptionTests()
		{
			_forwardingAppender = new ForwardingAppender();
			_observable = _forwardingAppender.ToObservable();
			_log = LogManager.GetLogger(typeof (RxInterceptionTests<T>));
			_logLevel = new T();
		}

		[TestFixtureSetUp]
		public void Configure_log4net()
		{
			BasicConfigurator.Configure(_forwardingAppender);
		}

		[SetUp]
		public void Create_testable_appender()
		{
			_testableAppender = new TestableAppender();
		}

		[Test]
		public void Can_intercept_log4net()
		{
			Assert.That(_logLevel.IsEnabled(_log));
			using (_observable.Subscribe(_testableAppender))
			{
				var message = new object();
				_logLevel.Log(_log, message);
				var loggingEvents = _testableAppender.GetEvents();
				Assert.That(loggingEvents.Length, Is.EqualTo(1));
				var loggingEvent = loggingEvents[0];
				Assert.That(loggingEvent.MessageObject, Is.EqualTo(message));
				Assert.That(loggingEvent.ExceptionObject, Is.Null);
				Assert.That(loggingEvent.Level, Is.EqualTo(_logLevel.Level));
			}
		}

		[Test]
		public void Can_intercept_log4net_with_exceptions()
		{
			Assert.That(_logLevel.IsEnabled(_log));
			using (_observable.Subscribe(_testableAppender))
			{
				var message = new object();
				var exception = new Exception();
				_logLevel.Log(_log, message, exception);
				var loggingEvents = _testableAppender.GetEvents();
				Assert.That(loggingEvents.Length, Is.EqualTo(1));
				var loggingEvent = loggingEvents[0];
				Assert.That(loggingEvent.MessageObject, Is.EqualTo(message));
				Assert.That(loggingEvent.ExceptionObject, Is.EqualTo(exception));
				Assert.That(loggingEvent.Level, Is.EqualTo(_logLevel.Level));
			}
		}
	}
}