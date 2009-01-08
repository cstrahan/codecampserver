using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using NHibernate;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class RepositoryBase<T> : RepositoryBase, IRepository<T> where T : PersistentObject
	{
		public RepositoryBase(ISessionBuilder sessionFactory)
			: base(sessionFactory)
		{
		}

		public virtual T GetById(Guid id)
		{
			ISession session = GetSession();
			return session.Load<T>(id);
		}

		public virtual void Save(T entity)
		{
			using (ISession session = GetSession())
			using (ITransaction tx = session.BeginTransaction())
			{
				session.SaveOrUpdate(entity);
				tx.Commit();
			}
		}

		public virtual T[] GetAll()
		{
			ISession session = GetSession();
			ICriteria criteria = session.CreateCriteria(typeof (T));
			return criteria.List<T>().ToArray();
		}
	}
}