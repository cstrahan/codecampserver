using System;
using System.IO;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace CodeCampServer.Website.Helpers
{
	public class IoC
	{
		private static IWindsorContainer _container;

		private static readonly object _lockDummy = new object();

		private static bool _initiliazed;

		public const string WindsorConfigFilename = @"Windsor.config.xml";

		private static void EnsureInitialized()
		{
			if (!_initiliazed)
			{
				lock (_lockDummy)
				{
					if (!_initiliazed)
					{
						var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
						var configPath = Path.Combine(basePath, WindsorConfigFilename);
						if(!File.Exists(configPath))
						{
							basePath = basePath + @"bin\";
							configPath = Path.Combine(basePath, WindsorConfigFilename);
						}

						var configInterpreter = new XmlInterpreter(configPath);
						InitializeWith(new WindsorContainer(configInterpreter));
						_initiliazed = true;
					}
				}
			}
		}

		public static void InitializeWith(IWindsorContainer container)
		{
			_container = container;
			_initiliazed = true;
		}

		public static T Resolve<T>()
		{
			EnsureInitialized();
			return _container.Resolve<T>();
		}

		public static T Resolve<T>(string key)
		{
			EnsureInitialized();
			return _container.Resolve<T>(key);
		}

		public static void Register<Interface, Implementation>()
		{
			EnsureInitialized();
			Register<Interface, Implementation>(typeof (Interface).Name);
		}

		public static void Register<Interface, Implementation>(string key)
		{
			EnsureInitialized();
			_container.AddComponent(key, typeof (Interface), typeof (Implementation));
		}

		public static void Register(string key, Type type, LifestyleType lifestyleType)
		{
			EnsureInitialized();
			_container.AddComponentWithLifestyle(key, type, lifestyleType);
		}

		public static IWindsorContainer GetContainer()
		{
			EnsureInitialized();
			return _container;
		}

		public static void Reset()
		{
			if (_container != null)
			{
				_container.Dispose();
				_container = null;
			}
		}
	}
}