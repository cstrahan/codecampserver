using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class MeetingMapper : AutoInputMapper<Meeting, MeetingInput>, IMeetingMapper
	{
		private readonly IUserGroupRepository _groupRepository;

		public MeetingMapper(IMeetingRepository repository, IUserGroupRepository groupRepository)
			: base(repository)
		{
			_groupRepository = groupRepository;
		}

		protected override Guid GetIdFromMessage(MeetingInput message)
		{
			return message.Id;
		}

		protected override void MapToModel(MeetingInput input, Meeting model)
		{
			model.Key = input.Key;
			model.Name = input.Name;
			model.Description = input.Description;
			model.StartDate = input.StartDate;
			model.EndDate = input.EndDate;
			model.LocationName = input.LocationName;
			model.LocationUrl = input.LocationUrl;
//			model.Address = input.Address;
//			model.City = input.City;
//			model.Region = input.Region;
//			model.PostalCode = input.PostalCode;
			model.UserGroup = _groupRepository.GetById(input.UserGroupId);
			model.TimeZone = input.TimeZone;
			model.Topic = input.Topic;
			model.Summary = input.Summary;
			model.SpeakerName = input.SpeakerName;
			model.SpeakerBio = input.SpeakerBio;
			model.SpeakerUrl = input.SpeakerUrl;
		}
	}
}