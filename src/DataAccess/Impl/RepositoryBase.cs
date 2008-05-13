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
        
        protected ISession getSession()
        {
            ISession session = _sessionBuilder.GetSession();
            return session;
        }

        public void Flush()
        {            
            getSession().Flush();
        }
    }
}