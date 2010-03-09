using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases;
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
			this.ChangeAuditInfo();
		}
	}
}