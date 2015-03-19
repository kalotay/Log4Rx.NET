using System;
using log4net.Appender;
using log4net.Core;

namespace Log4Rx
{
	public class ObserverAppender: AppenderSkeleton
	{
		private readonly IObserver<LoggingEvent> _observer;

		public ObserverAppender(IObserver<LoggingEvent> observer)
		{
			_observer = observer;
		}

		protected override void OnClose()
		{
			_observer.OnCompleted();
			base.OnClose();
		}

		protected override void Append(LoggingEvent loggingEvent)
		{
			_observer.OnNext(loggingEvent);
		}
	}
}