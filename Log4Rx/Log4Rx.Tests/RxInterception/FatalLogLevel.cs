using System;
using log4net;
using log4net.Core;

namespace Log4Rx.Tests.RxInterception
{
	public class FatalLogLevel: ILogLevel
	{
		public Level Level { get { return Level.Fatal; } }
		public bool IsEnabled(ILog log)
		{
			return log.IsFatalEnabled;
		}

		public void Log(ILog log, object message)
		{
			log.Fatal(message);
		}

		public void Log(ILog log, object message, Exception exception)
		{
			log.Fatal(message, exception);
		}
	}
}