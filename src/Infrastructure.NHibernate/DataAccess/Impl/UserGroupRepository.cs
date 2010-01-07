using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl
{
	public class UserGroupRepository : KeyedRepository<UserGroup>, IUserGroupRepository
	{
	    public override UserGroup GetByKey(string key)
		{
			return
				GetSession().CreateCriteria(typeof (UserGroup)).SetFetchMode("Sponsors", FetchMode.Eager).Add(
					Restrictions.Eq(GetEntityNaturalKeyName(), key)).UniqueResult<UserGroup>();
		}

		public UserGroup GetDefaultUserGroup()
		{
			return GetByKey("localhost");
		}

		public override void Save(UserGroup entity)
		{
			foreach (var sponsor in entity.GetSponsors())
			{
				GetSession().SaveOrUpdate(sponsor);
			}
			base.Save(entity);
		}
	}
}