using System.Reactive;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using log4net.Core;

namespace Log4Rx.Tests
{
	[TestFixture]
	public class ColdObservableAppenderSubscriptionTests : ObservableAppenderSubscriptionTests
	{
		protected override ITestableObservable<LoggingEvent> CreateObservable(TestScheduler scheduler, params Recorded<Notification<LoggingEvent>>[] events)
		{
			return scheduler.CreateColdObservable(events);
		}
	}
}