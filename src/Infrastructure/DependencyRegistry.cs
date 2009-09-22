using CodeCampServer.Core.Domain;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.Infrastructure
{
	public class DependencyRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType(typeof (IRepository<>)).TheDefaultIsConcreteType(typeof (RepositoryBase<>));
			ForRequestedType(typeof (IKeyedRepository<>)).TheDefaultIsConcreteType(typeof (KeyedRepository<>));
            ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
        }
	}
}