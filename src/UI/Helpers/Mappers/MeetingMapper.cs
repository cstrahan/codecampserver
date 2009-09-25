using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class MeetingMapper : AutoFormMapper<Meeting, MeetingForm>, IMeetingMapper
	{
		private readonly IUserGroupRepository _groupRepository;

		public MeetingMapper(IMeetingRepository repository, IUserGroupRepository groupRepository)
			: base(repository)
		{
			_groupRepository = groupRepository;
		}

		protected override Guid GetIdFromMessage(MeetingForm message)
		{
			return message.Id;
		}

		protected override void MapToModel(MeetingForm form, Meeting model)
		{
			model.Key = form.Key;
			model.Name = form.Name;
			model.Description = form.Description;
			model.StartDate = form.StartDate;
			model.EndDate = form.EndDate;
			model.LocationName = form.LocationName;
			model.LocationUrl = form.LocationUrl;
			model.Address = form.Address;
			model.City = form.City;
			model.Region = form.Region;
			model.PostalCode = form.PostalCode;
			model.UserGroup = _groupRepository.GetById(form.UserGroupId);
			model.TimeZone = form.TimeZone;
			model.Topic = form.Topic;
			model.Summary = form.Summary;
			model.SpeakerName = form.SpeakerName;
			model.SpeakerBio = form.SpeakerBio;
			model.SpeakerUrl = form.SpeakerUrl;
		}
	}
}