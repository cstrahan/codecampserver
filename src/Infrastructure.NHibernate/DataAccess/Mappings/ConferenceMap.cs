using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
{
	public class ConferenceMap : SubclassMap<Conference>
	{
		public ConferenceMap()
		{
			Table("Conferences");
			Map(x => x.PhoneNumber);
			Map(x => x.HtmlContent).CustomType("StringClob").CustomSqlType("ntext");
			Map(x => x.HasRegistration);

			References(x => x.UserGroup);
			this.ChangeAuditInfo();
		}
	}

	/*
	<joined-subclass name="Conference" extends="Event" table="Conferences" dynamic-update="true">
    <key column="Id"/>
    <property name="PhoneNumber" />
    <property name="HtmlContent" type="StringClob">
      <column name="HtmlContent" sql-type="ntext"/>
    </property>
    <property name="HasRegistration" />

    <many-to-one name="UserGroup" column="UserGroupID" class="UserGroup" />

  </joined-subclass>*/


}