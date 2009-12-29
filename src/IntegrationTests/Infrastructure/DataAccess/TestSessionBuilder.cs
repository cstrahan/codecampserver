using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NHibernate;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class TestSessionBuilder : ISessionBuilder
	{
		private readonly ISessionFactory _sessionFactory;
		private ISession _session;

		public TestSessionBuilder(ISessionFactory sessionFactory)
		{
			_sessionFactory = sessionFactory;
		}

		public ISession GetSession()
		{
			if ((_session == null) || (!_session.IsOpen))
			{
				_session = _sessionFactory.OpenSession();
			}
			return _session;
		}
	}
}