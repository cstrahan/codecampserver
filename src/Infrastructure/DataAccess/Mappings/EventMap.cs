using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
{
	public class EventMap : AuditedEntityClassMap<Event>
	{
		public EventMap()
		{
			Table("Events");
			Map(x => x.Key).Column("EventKey").Length(100);
			Map(x => x.Name).Length(100);
			Map(x => x.Description).Length(100);
			Map(x => x.StartDate);
			Map(x => x.EndDate);
			Map(x => x.LocationName);
			Map(x => x.LocationUrl);
			Map(x => x.Address);
			Map(x => x.City);
			Map(x => x.Region);
			Map(x => x.PostalCode);
			Map(x => x.TimeZone);
			References(x => x.UserGroup);
		}
	}

/*
		<class name="Event" abstract="true" table="Events" dynamic-update="true">
		<cache usage="read-write"/>
		<id name="Id" column="Id" type="Guid">
			<generator class="guid.comb"/>
		</id>
		<property name="Key" column="EventKey" length="100" />
		<property name="Name" length="100"/>
		<property name="Description" length="500"/>
		<property name="StartDate" />
		<property name="EndDate" />
    <property name="LocationName" />
    <property name="LocationUrl"/>
		<property name="Address" />
		<property name="City" />
		<property name="Region" />
		<property name="PostalCode" />
    <property name="TimeZone" />

    <many-to-one name="UserGroup" column="UserGroupID" class="UserGroup" />
    
	</class>*/
}