using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Infrastructure.CommandProcessor;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.DependencyResolution
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			For(typeof (IRepository<>)).Use(typeof (RepositoryBase<>));
			For(typeof (IKeyedRepository<>)).Use(typeof (KeyedRepository<>));
			For<IMessageMapper>().Use<RulesEngineConfiguration.CcsMessageMapper>();
			Scan(x =>
			     	{
			     		x.AssemblyContainingType<Event>();
			     		x.ConnectImplementationsToTypesClosing(typeof (Command<>));
						x.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
			     	});
		}
	}
}