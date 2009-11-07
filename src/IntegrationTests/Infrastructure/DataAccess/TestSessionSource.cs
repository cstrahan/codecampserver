using CodeCampServer.Infrastructure.DataAccess;
using NHibernate;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class TestSessionSource : ISessionSource
	{
		private readonly ISessionFactory _sessionFactory;
		private ISession _session;

		public TestSessionSource(ISessionFactory sessionFactory)
		{
			_sessionFactory = sessionFactory;
		}

		public ISession CreateSession()
		{
			if ((_session == null) || (!_session.IsOpen))
			{
				_session = _sessionFactory.OpenSession();
			}
			return _session;
		}
	}
}