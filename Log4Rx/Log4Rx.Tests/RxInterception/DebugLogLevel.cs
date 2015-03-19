using System;
using log4net;
using log4net.Core;

namespace Log4Rx.Tests.RxInterception
{
	public class DebugLogLevel: ILogLevel
	{
		public Level Level { get { return Level.Debug; } }
		public bool IsEnabled(ILog log)
		{
			return log.IsDebugEnabled;
		}

		public void Log(ILog log, object message)
		{
			log.Debug(message);
		}

		public void Log(ILog log, object message, Exception exception)
		{
			log.Debug(message, exception);
		}
	}
}