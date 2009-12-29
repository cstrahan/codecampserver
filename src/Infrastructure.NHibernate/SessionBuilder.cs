using System;
using CodeCampServer.Core;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public class SessionBuilder : AbstractFactoryBase<ChangeAuditInfoInterceptor>, DataAccess.ISessionBuilder
	{
		private const string NHIBERNATE_SESSION = "NHibernate.ISession";
		private readonly HybridInstanceScoper<ISession> _hybridInstanceScoper;
		private readonly ISessionFactoryBuilder _builder;
		public static Func<ChangeAuditInfoInterceptor> GetDefault = DefaultUnconfiguredState;

		public SessionBuilder()
		{
			_hybridInstanceScoper = new HybridInstanceScoper<ISession>();
			_builder = new SessionFactoryBuilder();
		}

		public ISession GetSession()
		{
			ISession instance = GetScopedInstance();
			if(!instance.IsOpen)
			{
				_hybridInstanceScoper.ClearScopedInstance(NHIBERNATE_SESSION);
				return GetScopedInstance();
			}
			return instance;
		}

		private ISession GetScopedInstance()
		{
			return _hybridInstanceScoper.GetScopedInstance(NHIBERNATE_SESSION, BuildSession);
		}

		private ISession BuildSession()
		{
			var interceptor = GetDefault();

			ISessionFactory factory = _builder.GetFactory();
			ISession session = interceptor == null ? factory.OpenSession() : factory.OpenSession(interceptor);
			session.FlushMode = FlushMode.Commit;
			return session;
		}
	}
}