using System.Reactive;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using log4net.Core;
using log4net.Util;

namespace Log4Rx.Tests
{
	[TestFixture]
	public class Log4NetRxAdapterTests
	{
		private readonly IAppenderAttachable _appenderAttachable;
		private readonly TestScheduler _scheduler;

		private ITestableObserver<LoggingEvent> _observer;

		public Log4NetRxAdapterTests()
		{
			_appenderAttachable = new AppenderAttachedImpl();
			_scheduler = new TestScheduler();
		}

		[SetUp]
		public void Remove_appenders_and_reset_observer()
		{
			_appenderAttachable.RemoveAllAppenders();
			_observer = _scheduler.CreateObserver<LoggingEvent>();
		}

		[Test]
		public void Can_turn_appender_attachable_to_observable()
		{
			Assert.That(_appenderAttachable.Appenders.Count, Is.EqualTo(0));
			var observable = _appenderAttachable.ToObservable();
			using (observable.Subscribe(_observer))
			{
				Assert.That(_appenderAttachable.Appenders.Count, Is.EqualTo(1));
			}
			Assert.That(_appenderAttachable.Appenders.Count, Is.EqualTo(0));
		}

		[Test]
		public void Attached_appender_delegates_append_to_observer_onnext()
		{
			var observable = _appenderAttachable.ToObservable();
			var @event = new LoggingEvent(new LoggingEventData());
			using (observable.Subscribe(_observer))
			{
				var appenders = _appenderAttachable.Appenders;
				Assert.That(appenders.Count, Is.EqualTo(1));
				var appender = appenders[0];
				appender.DoAppend(@event);
			}
			Assert.That(_observer.Messages.Count, Is.EqualTo(1));
			Assert.That(_observer.Messages[0].Value.Value, Is.EqualTo(@event));
		}

		[Test]
		public void Attached_appender_delegates_close_to_observer_oncompleted()
		{
			var observable = _appenderAttachable.ToObservable();
			using (observable.Subscribe(_observer))
			{
				var appenders = _appenderAttachable.Appenders;
				Assert.That(appenders.Count, Is.EqualTo(1));
				var appender = appenders[0];
				appender.Close();
			}
			Assert.That(_observer.Messages.Count, Is.EqualTo(1));
			Assert.That(_observer.Messages[0].Value.Kind, Is.EqualTo(NotificationKind.OnCompleted));
		}
	}
}