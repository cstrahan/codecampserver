using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain;
using NHibernate.Criterion;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl
{
	public class KeyedRepository<T> : RepositoryBase<T>, IKeyedRepository<T> where T : PersistentObject
	{
		protected const string KEY_NAME = "Key";

	    public virtual T GetByKey(string key)
		{
			return GetSession().CreateCriteria(typeof (T))
				.Add(Restrictions.Eq(GetEntityNaturalKeyName(), key))
				.UniqueResult<T>();
		}

		protected virtual string GetEntityNaturalKeyName()
		{
			return KEY_NAME;
		}
	}
}