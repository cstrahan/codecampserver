using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
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

	/*<class name="User" table="Users" dynamic-update="true">
		<cache usage="read-write"/>
		<id name="Id" column="Id" type="Guid">
			<generator class="guid.comb"/>
		</id>

		<property name="Username" length="50"/>
		<property name="Name" length="100"/>
		<property name="EmailAddress" length="100"/>
		<property name="PasswordHash" length="100"/>
		<property name="PasswordSalt" length="100"/>
	</class>*/
}