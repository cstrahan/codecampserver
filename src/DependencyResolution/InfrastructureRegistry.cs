using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using StructureMap.Configuration.DSL;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Infrastructure
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			ForRequestedType(typeof (IRepository<>)).TheDefaultIsConcreteType(typeof (RepositoryBase<>));
			ForRequestedType(typeof (IKeyedRepository<>)).TheDefaultIsConcreteType(typeof (KeyedRepository<>));

			Scan(x =>
			     	{
			     		x.AssemblyContainingType<Event>();
			     		x.ConnectImplementationsToTypesClosing(typeof (Command<>));
			     	});
		}
	}


	public class AutoMapperRegistry : Registry
	{
		public AutoMapperRegistry()
		{
			ForRequestedType<IMappingEngine>().TheDefault.Is.ConstructedBy(() => Mapper.Engine);
		}
	}
}