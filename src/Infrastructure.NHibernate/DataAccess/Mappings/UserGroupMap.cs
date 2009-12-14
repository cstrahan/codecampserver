using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Mappings
{
	public class UserGroupMap : AuditedEntityClassMap<UserGroup>
	{
		public UserGroupMap()
		{
			Table("UserGroups");
			Map(x => x.Key).Column("UserGroupKey").Length(100);
			Map(x => x.Name).Length(100);
			Map(x => x.HomepageHTML).CustomType("StringClob").Column("HomepageHTML").CustomSqlType("ntext");
			Map(x => x.City).Length(75);
			Map(x => x.Region).Length(50);
			Map(x => x.Country).Length(50);
			Map(x => x.GoogleAnalysticsCode).Length(50);
			Map(x => x.DomainName).Length(255);

			HasManyToMany(x => x.GetUsers())
				.Access.CamelCaseField(Prefix.Underscore)
				.Table("UserGroupAdminUsers")
				.ParentKeyColumn("UserGroupId")
				.ChildKeyColumn("UserID")
				.AsBag();

			HasMany(x => x.GetSponsors())
				.Access.CamelCaseField(Prefix.Underscore)
				.Table("UserGroupSponsors")
				.KeyColumn("UserGroupId")
				.AsSet();
		}
	}

/*
		<class name="UserGroup" table="UserGroups" dynamic-update="true">		
		<id name="Id" column="Id" type="Guid">
			<generator class="guid.comb"/>
		</id>
		<property name="Key" column="UserGroupKey" length="100"/>
		<property name="Name" length="100"/>
    <property name="HomepageHTML" type="StringClob">
      <column name="HomepageHTML" sql-type="ntext"/>
    </property> 
    <property name="City" length="75" />
    <property name="Region" length="50" />
    <property name="Country" length="50" />
    <property name="GoogleAnalysticsCode" length="50" />
    <property name="DomainName" length="255" />
   
    <bag name="Users" access="field.camelcase-underscore"  table="UserGroupAdminUsers" >
			<key column="UserGroupId"  />
			<many-to-many class="User" column="UserID"/>
		</bag>
    <set name="Sponsors" access="field.camelcase-underscore"  table="UserGroupSponsors">
      <key column="UserGroupId"  />
      <one-to-many class="Sponsor" />
    </set>
  </class>
*/
}