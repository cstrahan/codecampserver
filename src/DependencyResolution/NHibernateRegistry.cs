using NHibernate;
using NHibernate.Cfg;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class NHibernateRegistry : Registry
	{
		public NHibernateRegistry()
		{
			var cfg = ConfigurationFactory.Build();
			var sessionFactory = cfg.BuildSessionFactory();

			ForRequestedType<Configuration>().AsSingletons()
				.TheDefault.IsThis(cfg);

			ForRequestedType<ISessionFactory>().AsSingletons()
				.TheDefault.IsThis(sessionFactory);

			ForRequestedType<ISession>().CacheBy(InstanceScope.Hybrid)
				.TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());

			ForRequestedType<ISessionSource>()
				.TheDefaultIsConcreteType<ConfigBasedSessionSource>()
				.AsSingletons();

			ForRequestedType<IUnitOfWork>()
				.TheDefaultIsConcreteType<UnitOfWork>()
				.CacheBy(InstanceScope.Hybrid);

			ForRequestedType<Tarantino.RulesEngine.IUnitOfWork>().TheDefault.Is.ConstructedBy(
				ctx => ctx.GetInstance<IUnitOfWork>());
		}
	}
}