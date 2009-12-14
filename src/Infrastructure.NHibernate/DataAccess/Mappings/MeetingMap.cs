using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Mappings
{
	public class MeetingMap : SubclassMap<Meeting>
	{
		public MeetingMap()
		{
			Table("Meetings");
			Map(x => x.Topic).Length(1000);
			Map(x => x.Summary).Length(1000);
			Map(x => x.SpeakerName);
			Map(x => x.SpeakerBio).Length(1000);
			Map(x => x.SpeakerUrl).Length(255);
		    Map(x => x.Refreshments);
			this.ChangeAuditInfo();
		}
	}

/*	  <joined-subclass name="Meeting" extends="Event" table="Meetings" dynamic-update="true">
    <key column="Id"/>
    <property name="Topic" length="1000"/>
    <property name="Summary" length="1000"/>
    <property name="SpeakerName"/>
    <property name="SpeakerBio" length="1000"/>
    <property name="SpeakerUrl" length="255"/>
  </joined-subclass>*/
}