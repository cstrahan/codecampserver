using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class RepositoryBase<T> :  IRepository<T> where T : PersistentObject
	{
		private readonly IUnitOfWork _unitOfWork;

		public RepositoryBase(IUnitOfWork unitOfWork )
		{
			_unitOfWork = unitOfWork;
		}

		public virtual T GetById(Guid id)
		{
			ISession session = GetSession();
			return session.Get<T>(id);
		}

		protected ISession GetSession()
		{
			return _unitOfWork.CurrentSession;
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