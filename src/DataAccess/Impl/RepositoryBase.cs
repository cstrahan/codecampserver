using NHibernate;

namespace CodeCampServer.DataAccess.Impl
{
    public class RepositoryBase
    {
        private readonly ISessionBuilder _sessionBuilder;

        public RepositoryBase(ISessionBuilder sessionFactory)
        {
            _sessionBuilder = sessionFactory;
        }

        protected ISession getSession(Database selectedDatabase)
        {
            ISession session = _sessionBuilder.GetSession(selectedDatabase);
            return session;
        }
    }
}