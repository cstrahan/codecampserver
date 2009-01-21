using System;
using System.Web;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistrarModule : IHttpModule
	{
		private static bool _dependenciesRegistered;

		public void Init(HttpApplication context)
		{
			context.BeginRequest += context_BeginRequest;
		}

		public void Dispose()
		{
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			EnsureDependenciesRegistered();
		}

		private void EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				lock (this)
				{
					if (!_dependenciesRegistered)
					{
						Logger.Debug(this, "Registering types with StructureMap");
						ObjectFactory.Reset();
						var registrar = new DependencyRegistrar();
						registrar.RegisterDependencies();
						_dependenciesRegistered = true;

						
					}
				}
			}
		}
	}
}