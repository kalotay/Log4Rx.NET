using System;
using log4net;
using log4net.Core;

namespace Log4Rx.Tests.RxInterception
{
	public class WarnLogLevel: ILogLevel
	{
		public Level Level { get { return Level.Warn; } }
		public bool IsEnabled(ILog log)
		{
			return log.IsWarnEnabled;
		}

		public void Log(ILog log, object message)
		{
			log.Warn(message);
		}

		public void Log(ILog log, object message, Exception exception)
		{
			log.Warn(message, exception);
		}
	}
}