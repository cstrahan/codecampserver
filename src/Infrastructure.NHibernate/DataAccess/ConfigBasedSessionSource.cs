using System;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class ConfigBasedSessionSource : ISessionSource
	{
		public static Func<Type, object> CreateDependencyCallback = (type) => Activator.CreateInstance(type);

		private T CreateDependency<T>()
		{
			return (T)CreateDependencyCallback(typeof(T));
		}

		private readonly ISessionFactory _sessionFactory;

		public ConfigBasedSessionSource(Configuration configuration)
		{
			_sessionFactory = configuration.BuildSessionFactory();
		}

		public ISession CreateSession()
		{
			var interceptor = CreateDependency<ChangeAuditInfoInterceptor>();

			ISession session = _sessionFactory.OpenSession(interceptor);
			session.FlushMode = FlushMode.Commit;
			return session;
		}
	}
}