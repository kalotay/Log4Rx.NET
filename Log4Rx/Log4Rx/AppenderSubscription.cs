using System;
using log4net.Appender;
using log4net.Core;

namespace Log4Rx
{
	public static class AppenderSubscription
	{
		public static IDisposable Subscribe(this IObservable<LoggingEvent> observable, IAppender appender)
		{
			return observable.Subscribe(appender.DoAppend, appender.Close);
		}

		public static IDisposable Subscribe(this IObservable<LoggingEvent[]> observable, IBulkAppender appender)
		{
			return observable.Subscribe(appender.DoAppend, appender.Close);
		}
	}
}