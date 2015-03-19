using System;
using log4net;
using log4net.Core;

namespace Log4Rx.Tests.RxInterception
{
	public interface ILogLevel
	{
		Level Level { get; }
		bool IsEnabled(ILog log);
		void Log(ILog log, object message);
		void Log(ILog log, object message, Exception exception);
	}
}