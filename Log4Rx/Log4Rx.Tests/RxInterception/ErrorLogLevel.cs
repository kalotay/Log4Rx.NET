using System;
using log4net;
using log4net.Core;

namespace Log4Rx.Tests.RxInterception
{
	public class ErrorLogLevel: ILogLevel
	{
		public Level Level { get { return Level.Error; } }
		public bool IsEnabled(ILog log)
		{
			return log.IsErrorEnabled;
		}

		public void Log(ILog log, object message)
		{
			log.Error(message);
		}

		public void Log(ILog log, object message, Exception exception)
		{
			log.Error(message, exception);
		}
	}
}