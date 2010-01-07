using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl
{
    public class RepositoryBase<T> : IRepository<T> where T : PersistentObject
    {
        public virtual T GetById(object id)
        {
            ISession session = GetSession();
            return session.Get<T>(id);
        }

        protected ISession GetSession()
        {
            return new SessionBuilder().GetSession();
        }

        public virtual void Save(T entity)
        {
            GetSession().SaveOrUpdate(entity);
        }

        public virtual T[] GetAll()
        {
            ISession session = GetSession();
            ICriteria criteria = session.CreateCriteria(typeof (T));
            return criteria.List<T>().ToArray();
        }

        public virtual void Delete(T entity)
        {
            GetSession().Delete(entity);
        }
    }
}