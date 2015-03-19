using System.Collections;
using log4net.Appender;

namespace Log4Rx.Tests
{
	public class TestableAppender: MemoryAppender
	{
		protected override void OnClose()
		{
			Closed = true;
			base.OnClose();
		}

		public bool Closed { get; private set; }
	}
}