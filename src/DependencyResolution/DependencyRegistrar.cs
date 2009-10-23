using System;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistrar
	{
		private static bool _dependenciesRegistered;
		private static readonly object sync = new object();

		public void RegisterDependencies()
		{
			ObjectFactory.Initialize(x =>
			                         	{
			                         		x.Scan(y =>
			                         		       	{
			                         		       		y.AssemblyContainingType<DependencyRegistry>();
			                         		       		y.LookForRegistries();
			                         		       	});

			                         	});
		}

		public void ConfigureOnStartup()
		{
			RegisterDependencies();
			var dependenciesToInitialized = ObjectFactory.GetAllInstances<IRequiresConfigurationOnStartup>();
			foreach (var dependency in dependenciesToInitialized)
			{
				dependency.Configure();
			}
		}
		public static T Resolve<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}

		public static object Resolve(Type modelType)
		{
			return ObjectFactory.GetInstance(modelType);
		}

		//public static bool Registered<T>()
		//{
		//    EnsureDependenciesRegistered();
		//    return ObjectFactory.GetInstance<T>() != null;
		//}

		public static bool Registered(Type type)
		{
			EnsureDependenciesRegistered();
			return ObjectFactory.GetInstance(type) != null;
		}

		public static void EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				lock (sync)
				{
					if (!_dependenciesRegistered)
					{
						new DependencyRegistrar().RegisterDependencies();
						_dependenciesRegistered = true;
						
					}
				}
			}
		}
	}
}