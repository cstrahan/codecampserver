using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ConferenceMapper : AutoInputMapper<Conference, ConferenceInput>, IConferenceMapper
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public ConferenceMapper(IConferenceRepository repository, IUserGroupRepository userGroupRepository)
			: base(repository)
		{
			_userGroupRepository = userGroupRepository;
		}


		protected override Guid GetIdFromMessage(ConferenceInput message)
		{
			return message.Id;
		}

		protected override void MapToModel(ConferenceInput input, Conference model)
		{
			model.Key = input.Key;
			model.Name = input.Name;
			model.Description = input.Description;
			model.StartDate = input.StartDate;
			model.EndDate = input.EndDate;
			model.LocationName = input.LocationName;
			model.LocationUrl = input.LocationUrl;
			model.Address = input.Address;
			model.City = input.City;
			model.Region = input.Region;
			model.PostalCode = input.PostalCode;
			model.UserGroup = _userGroupRepository.GetById(input.UserGroupId);
			model.TimeZone = input.TimeZone;
			model.PhoneNumber = input.PhoneNumber;
			model.HtmlContent = input.HtmlContent;
			model.HasRegistration = input.HasRegistration;
		}
	}
}