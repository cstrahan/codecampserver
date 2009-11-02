using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using StructureMap;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Infrastructure
{
	public class IfrastructureRegistry : Registry
	{
		public IfrastructureRegistry()
		{
			ForRequestedType(typeof (IRepository<>)).TheDefaultIsConcreteType(typeof (RepositoryBase<>));
			ForRequestedType(typeof (IKeyedRepository<>)).TheDefaultIsConcreteType(typeof (KeyedRepository<>));

			ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
			ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<UnitOfWork>();
			ForRequestedType<Tarantino.RulesEngine.IUnitOfWork>()
				.TheDefault.Is.ConstructedBy(() => ObjectFactory.GetInstance<IUnitOfWork>());


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