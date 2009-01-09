using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate.Criterion;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class KeyedRepository<T> : RepositoryBase<T>, IKeyedRepository<T>
		where T : PersistentObject, IHasNaturalKey
	{
		public KeyedRepository(ISessionBuilder sessionFactory)
			: base(sessionFactory)
		{
		}

		public virtual T GetByKey(string key)
		{
			return
				GetSession().CreateCriteria(typeof (T)).Add(
					Restrictions.Eq(GetEntityNaturalKeyName(),
					                key)).
					UniqueResult<T>();
		}

		protected virtual string GetEntityNaturalKeyName()
		{
			return "Key";
		}
	}
}