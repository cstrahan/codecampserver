using System.Reflection;
using CodeCampServer.Infrastructure.DataAccess.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public static class ConfigurationFactory
	{
		public static Configuration Build()
		{
			return Build(null);
		}

		public static Configuration Build(string configurationFile)
		{
			var configuration = new Configuration();

			if (string.IsNullOrEmpty(configurationFile))
				configuration.Configure();
			else
				configuration.Configure(configurationFile);

			return Fluently.Configure(configuration)
				.Mappings(cfg =>
				          	{
								cfg.HbmMappings.AddFromAssembly(typeof(UserMap).Assembly);
								cfg.FluentMappings.AddFromAssembly(typeof(UserMap).Assembly)
				          			.Conventions.Setup(mappings =>
				          			                   	{
				          			                   		mappings.AddAssembly(typeof(UserMap).Assembly);
				          			                   		mappings.Add(ForeignKey.EndsWith("Id"));
				          			                   	});
				          	}).BuildConfiguration();
		}
	}
}