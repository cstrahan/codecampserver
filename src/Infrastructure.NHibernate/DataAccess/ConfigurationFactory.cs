using CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class ConfigurationFactory
	{
		public Configuration Build()
		{
			var configuration = new Configuration();
			configuration.Configure();

			return Fluently.Configure(configuration)
				.Mappings(cfg =>
				          	{
				          		cfg.HbmMappings.AddFromAssembly(typeof (UserMap).Assembly);
				          		cfg.FluentMappings.AddFromAssembly(typeof (UserMap).Assembly)
				          			.Conventions.Setup(mappings =>
				          			                   	{
				          			                   		mappings.AddAssembly(typeof (UserMap).Assembly);
				          			                   		mappings.Add(ForeignKey.EndsWith("Id"));
				          			                   	});
				          	}).BuildConfiguration();
		}
	}
}