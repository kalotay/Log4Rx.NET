using System;
using log4net;
using log4net.Core;

namespace Log4Rx.Tests.RxInterception
{
	public class InfoLogLevel: ILogLevel
	{
		public Level Level { get { return Level.Info; } }
		public bool IsEnabled(ILog log)
		{
			return log.IsInfoEnabled;
		}

		public void Log(ILog log, object message)
		{
			log.Info(message);
		}

		public void Log(ILog log, object message, Exception exception)
		{
			log.Info(message, exception);
		}
	}
}