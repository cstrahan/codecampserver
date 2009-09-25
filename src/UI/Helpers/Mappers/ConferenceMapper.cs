using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ConferenceMapper : AutoFormMapper<Conference, ConferenceForm>, IConferenceMapper
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public ConferenceMapper(IConferenceRepository repository, IUserGroupRepository userGroupRepository)
			: base(repository)
		{
			_userGroupRepository = userGroupRepository;
		}


		protected override Guid GetIdFromMessage(ConferenceForm message)
		{
			return message.Id;
		}

		protected override void MapToModel(ConferenceForm form, Conference model)
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
			model.UserGroup = _userGroupRepository.GetById(form.UserGroupId);
			model.TimeZone = form.TimeZone;
			model.PhoneNumber = form.PhoneNumber;
			model.HtmlContent = form.HtmlContent;
			model.HasRegistration = form.HasRegistration;
		}
	}
}