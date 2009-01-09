using CodeCampServer.Core.Domain;
using NHibernate.Criterion;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class KeyedRepository<T> : RepositoryBase<T>, IKeyedRepository<T> where T : PersistentObject
	{
		protected const string KEY_NAME = "Key";

		public KeyedRepository(ISessionBuilder sessionFactory)
			: base(sessionFactory)
		{
		}

		public virtual T GetByKey(string key)
		{
			return GetSession().CreateCriteria(typeof (T)).Add(Restrictions.Eq(GetEntityNaturalKeyName(), key)).UniqueResult<T>();
		}

		protected virtual string GetEntityNaturalKeyName()
		{
			return KEY_NAME;
		}
	}
}