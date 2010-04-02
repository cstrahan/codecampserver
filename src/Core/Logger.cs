using System;

namespace CodeCampServer.Core
{
	public class Logger : ILogger
	{
		public void Info(object source, object message)
		{
		}

		public void Warn(object source, object message)
		{
		}

		public void Error(object source, object message)
		{
		}

		public void Fatal(object source, object message)
		{
		}

		public void Debug(object source, object message)
		{
		}

		public void EnsureInitialized()
		{
		}

		public string SerializeException(Exception e)
		{
			return "";
		}
	}
}