using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
    public class RepositoryBase
    {
        private readonly ISessionBuilder _sessionBuilder;

        public RepositoryBase(ISessionBuilder sessionFactory)
        {
            _sessionBuilder = sessionFactory;
        }

        public RepositoryBase() : this(ObjectFactory.GetInstance<ISessionBuilder>())
        {
        }

        protected ISession getSession()
        {
            ISession session = _sessionBuilder.GetSession(Database.Default);
            return session;
        }

        public void Flush()
        {            
            getSession().Flush();
        }
    }
}