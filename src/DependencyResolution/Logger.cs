using System;
using System.Collections.Generic;
using System.IO;
using CodeCampServer.Core;
using log4net;
using log4net.Config;

namespace CodeCampServer.DependencyResolution
{
	public class Logger : ILogger
	{
		private static bool _logInitialized;
		public const string CONFIG_FILE_NAME = "Log4Net.config";
		private static readonly Dictionary<Type, ILog> _loggers = new Dictionary<Type, ILog>();
		Action<object, object> info = (source, message) => getLogger(source.GetType()).Info(message);
		Action<object, object> warn = (source, message) => getLogger(source.GetType()).Warn(message);
		Action<object, object> error = (source, message) => getLogger(source.GetType()).Error(message);
		Action<object, object> fatal = (source, message) => getLogger(source.GetType()).Fatal(message);
		Action<object, object> debug = (source, message) => getLogger(source.GetType()).Debug(message);

		private void initialize()
		{
			XmlConfigurator.ConfigureAndWatch(new FileInfo(getConfigFilePath()));
		}

		private string getConfigFilePath()
		{
			string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			string configPath = Path.Combine(basePath, CONFIG_FILE_NAME);

			if (!File.Exists(configPath))
			{
				configPath = Path.Combine(basePath, "bin");
				configPath = Path.Combine(configPath, CONFIG_FILE_NAME);

				if (!File.Exists(configPath))
				{
					configPath = Path.Combine(basePath, @"..\" + CONFIG_FILE_NAME);
				}
			}

			return configPath;
		}

		public void EnsureInitialized()
		{
			if (!_logInitialized)
			{
				initialize();
				_logInitialized = true;
			}
		}

		public string SerializeException(Exception e)
		{
			return serializeException(e, string.Empty);
		}

		private string serializeException(Exception e, string exceptionMessage)
		{
			if (e == null) return string.Empty;

			exceptionMessage = string.Format(
				"{0}{1}{2}\n{3}",
				exceptionMessage,
				(exceptionMessage == string.Empty) ? string.Empty : "\n\n",
				e.Message,
				e.StackTrace);

			if (e.InnerException != null)
				exceptionMessage = serializeException(e.InnerException, exceptionMessage);

			return exceptionMessage;
		}

		private static ILog getLogger(Type source)
		{
			if (!_loggers.ContainsKey(source))
			{
				lock (_loggers)
				{
					if (!_loggers.ContainsKey(source))
					{
						ILog logger = LogManager.GetLogger(source);
						_loggers.Add(source, logger);
					}
				}
			}


			return _loggers[source];
		}


		public void Info(object source, object message)
		{
			info(source, message);
		}

		public void Warn(object source, object message)
		{
			warn(source, message);
		}

		public void Error(object source, object message)
		{
			error(source, message);
		}

		public void Fatal(object source, object message)
		{
			fatal(source, message);
		}

		public void Debug(object source, object message)
		{
			debug(source, message);
		}

		public void SetInfo(Action<object, object> method)
		{
			info = method;
		}

		public void SetWarn(Action<object, object> method)
		{
			warn = method;
		}

		public void SetError(Action<object, object> method)
		{
			error = method;
		}

		public void SetFatal(Action<object, object> method)
		{
			fatal = method;
		}

		public void SetDebug(Action<object, object> method)
		{
			debug = method;
		}
	}
}