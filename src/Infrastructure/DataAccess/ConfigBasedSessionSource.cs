using CodeCampServer.DependencyResolution;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public class ConfigBasedSessionSource : ISessionSource
	{
		private readonly ISessionFactory _sessionFactory;

		public ConfigBasedSessionSource(Configuration configuration)
		{
			_sessionFactory = configuration.BuildSessionFactory();
		}

		public ISession CreateSession()
		{
			var interceptor = DependencyRegistrar.Resolve<ChangeAuditInfoInterceptor>();

			ISession session = _sessionFactory.OpenSession(interceptor);
			session.FlushMode = FlushMode.Commit;
			return session;
		}
	}
}