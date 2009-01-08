using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate.Criterion;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public abstract class KeyedRepositoryBase<T> : RepositoryBase<T>, IKeyedRepository<T> where T : KeyedObject
	{
		protected KeyedRepositoryBase(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		public virtual T GetByKey(string key)
		{
			return GetSession().CreateCriteria(typeof (T)).Add(Restrictions.Eq("Key", key)).UniqueResult<T>();
		}
	}
}