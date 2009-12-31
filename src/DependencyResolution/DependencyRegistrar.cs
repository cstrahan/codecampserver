using System;
using CodeCampServer.Core;
using Naak.HtmlRules;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistrar
	{
		private static bool _dependenciesRegistered;

		private static void RegisterDependencies()
		{
			ObjectFactory.Initialize(x => x.Scan(y =>
			                                     	{
			                                     		y.AssemblyContainingType<DependencyRegistry>();
			                                     		y.AssemblyContainingType<NaakRegistry>();
			                                     		y.LookForRegistries();
			                                     		y.AddAllTypesOf<IRequiresConfigurationOnStartup>();
			                                     	}));
			new InitiailizeDefaultFactories().Configure();

		}

		private static readonly object sync = new object();

		internal void ConfigureOnStartup()
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
						RegisterDependencies();
						_dependenciesRegistered = true;
						
					}
				}
			}
		}
	}
}