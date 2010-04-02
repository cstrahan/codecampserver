using System;

namespace CodeCampServer.Core
{
	public interface ILogger {
		void Info(object source, object message);
		void Warn(object source, object message);
		void Error(object source, object message);
		void Fatal(object source, object message);
		void Debug(object source, object message);
		void EnsureInitialized();
		string SerializeException(Exception e);
	}
}