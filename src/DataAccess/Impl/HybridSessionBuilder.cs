using System.Web;
using CodeCampServer.Model;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.DataAccess.Impl
{
	public class HybridSessionBuilder : ISessionBuilder
	{
		private static ISessionFactory _sessionFactory;
		private static ISession _currentSession;

		public ISession GetSession()
		{
			ISessionFactory factory = getSessionFactory();
			ISession session = getExistingOrNewSession(factory);
			Log.Debug(this, "Using ISession " + session.GetHashCode());
			return session;
		}

		private ISessionFactory getSessionFactory()
		{
			if (_sessionFactory == null)
			{
				Configuration configuration = GetConfiguration();
				_sessionFactory = configuration.BuildSessionFactory();
			}

			return _sessionFactory;
		}

		public Configuration GetConfiguration()
		{
			var configuration = new Configuration();
			configuration.Configure();
			return configuration;
		}

		private ISession getExistingOrNewSession(ISessionFactory factory)
		{
			if (HttpContext.Current != null)
			{
				ISession session = GetExistingWebSession();
				if (session == null)
				{
					session = openSessionAndAddToContext(factory);
				}
				else if (!session.IsOpen)
				{
					session = openSessionAndAddToContext(factory);
				}

				return session;
			}

			if (_currentSession == null)
			{
				_currentSession = factory.OpenSession();
			}
			else if (!_currentSession.IsOpen)
			{
				_currentSession = factory.OpenSession();
			}

			return _currentSession;
		}

		public ISession GetExistingWebSession()
		{
			return HttpContext.Current.Items[GetType().FullName] as ISession;
		}

		private ISession openSessionAndAddToContext(ISessionFactory factory)
		{
			ISession session = factory.OpenSession();
			HttpContext.Current.Items.Remove(GetType().FullName);
			HttpContext.Current.Items.Add(GetType().FullName, session);
			return session;
		}
	}
}