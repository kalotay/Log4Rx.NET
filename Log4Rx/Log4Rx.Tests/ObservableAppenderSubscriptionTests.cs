using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using log4net.Core;

namespace Log4Rx.Tests
{
	public abstract class ObservableAppenderSubscriptionTests
	{
		private TestableAppender _appender;
		private readonly TestScheduler _scheduler;

		protected ObservableAppenderSubscriptionTests()
		{
			_scheduler = new TestScheduler();
		}

		[SetUp]
		public void Create_appender()
		{
			_appender = new TestableAppender();
		}

		[Test]
		public void Can_subscribe_appender()
		{
			var loggingEvent = new LoggingEvent(new LoggingEventData());
			var append = ReactiveTest.OnNext(1, loggingEvent);
			var close = ReactiveTest.OnCompleted<LoggingEvent>(2);
			var observable = CreateObservable(_scheduler, append, close);
			Assert.That(observable.Subscriptions.Count, Is.EqualTo(0));
			using (observable.Subscribe(_appender))
			{
				Assert.That(_appender.Closed, Is.False);
				_scheduler.Start();
				Assert.That(observable.Subscriptions.Count, Is.EqualTo(1));
				Assert.That(_appender.GetEvents().Length, Is.EqualTo(1));
				Assert.That(_appender.Closed);
			}
		}

		[Test]
		public void Can_subscribe_bulk_appender()
		{
			var loggingEvent = new LoggingEvent(new LoggingEventData());
			var append = ReactiveTest.OnNext(1, loggingEvent);
			var close = ReactiveTest.OnCompleted<LoggingEvent>(2);
			var observable = CreateObservable(_scheduler, append, close).Select(le => new []{le});
			using (observable.Subscribe(_appender))
			{
				Assert.That(_appender.Closed, Is.False);
				_scheduler.Start();
				Assert.That(_appender.GetEvents().Length, Is.EqualTo(1));
				Assert.That(_appender.Closed);
			}
		}

		protected abstract ITestableObservable<LoggingEvent> CreateObservable(TestScheduler scheduler, params Recorded<Notification<LoggingEvent>>[] events);
	}
}