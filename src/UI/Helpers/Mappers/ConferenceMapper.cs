using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ConferenceMapper : AutoFormMapper<Conference, ConferenceForm>, IConferenceMapper
	{
	    private readonly IUserGroupRepository _userGroupRepository;

	    public ConferenceMapper(IConferenceRepository repository,IUserGroupRepository userGroupRepository) : base(repository)
		{
		    _userGroupRepository = userGroupRepository;
		}

	    protected override Guid GetIdFromMessage(ConferenceForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(ConferenceForm form, Conference model)
		{
			model.Address = form.Address;
			model.City = form.City;
			model.Key = form.Key;
			model.Description = form.Description;
			model.EndDate = ToDateTime(form.EndDate);
			model.LocationName = form.LocationName;
			model.Name = form.Name;
			model.PhoneNumber = form.PhoneNumber;
			model.PostalCode = form.PostalCode;
			model.Region = form.Region;
			model.StartDate = ToDateTime(form.StartDate);
		    model.HtmlContent = form.HtmlContent;
		    model.UserGroup = _userGroupRepository.GetById(form.UserGroupId);
		}
	}
}