using System;
using System.Reactive.Linq;
using log4net.Core;

namespace Log4Rx
{
	public static class AppenderAttachableToObservable
	{
		public static IObservable<LoggingEvent> ToObservable(this IAppenderAttachable appenderAttachable)
		{
			return Observable.Create<LoggingEvent>(observer =>
				{
					var appender = new ObserverAppender(observer);
					appenderAttachable.AddAppender(appender);
					return () => appenderAttachable.RemoveAppender(appender);
				});
		}
	}
}