using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
    [Pluggable(Keys.DEFAULT)]
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        public SessionRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        public void Save(Session session)
        {
            ISession repositorySession = getSession();
            ITransaction transaction = repositorySession.BeginTransaction();
            repositorySession.SaveOrUpdate(session);
            transaction.Commit();
        }

    }
}
