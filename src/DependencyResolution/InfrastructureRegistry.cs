using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.CommandProcessor;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using StructureMap.Configuration.DSL;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.DependencyResolution
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
            For(typeof(IRepository<>)).Use(typeof(RepositoryBase<>));
            For(typeof(IKeyedRepository<>)).Use(typeof(KeyedRepository<>));
            For<IMessageMapper>().Use<RulesEngineConfiguration.CcsMessageMapper>();
			Scan(x =>
			     	{
			     		x.AssemblyContainingType<Event>();
			     		x.ConnectImplementationsToTypesClosing(typeof (Command<>));
			     	});
		}
	}
}