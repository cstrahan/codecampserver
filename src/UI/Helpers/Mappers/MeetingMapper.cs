using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class MeetingMapper : EventMapperBase<Meeting, MeetingForm>, IMeetingMapper
	{
		public MeetingMapper(IMeetingRepository repository, IUserGroupRepository groupRepository)
			: base(repository, groupRepository) {}

		protected override void MapToModel(MeetingForm form, Meeting model)
		{
			base.MapToModel(form, model);
			model.Topic = form.Topic;
			model.Summary = form.Summary;
			model.SpeakerName = form.SpeakerName;
			model.SpeakerBio = form.SpeakerBio;
			model.SpeakerUrl = form.SpeakerUrl;
		}
	}
}