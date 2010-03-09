using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class UserMap : AuditedEntityClassMap<User>
	{
		public UserMap()
		{
			Table("Users");
			Map(x => x.Username).Length(50);
			Map(x => x.Name).Length(100);
			Map(x => x.EmailAddress).Length(100);
			Map(x => x.PasswordHash).Length(100);
			Map(x => x.PasswordSalt).Length(100);
		}
	}
}