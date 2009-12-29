using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public class SessionFactoryBuilder : ISessionFactoryBuilder
	{
		private const string SESSION_FACTORY = "sessionFactory";

		private readonly SingletonInstanceScoper<ISessionFactory> _sessionFactorySingleton =
			new SingletonInstanceScoper<ISessionFactory>();

		public ISessionFactory GetFactory()
		{
			return _sessionFactorySingleton.GetScopedInstance(SESSION_FACTORY, BuildFactory);
		}

		private ISessionFactory BuildFactory()
		{
			Configuration cfg = new ConfigurationFactory().Build();
			ISessionFactory sessionFactory = cfg.BuildSessionFactory();
			return sessionFactory;
		}
	}
}