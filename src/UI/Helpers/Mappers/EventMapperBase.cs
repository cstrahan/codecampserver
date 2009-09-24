using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public abstract class EventMapperBase<TModel, TForm> : AutoFormMapper<TModel, TForm> where TModel : Event, new()
	                                                                        where TForm : EventForm
	{
		private readonly IUserGroupRepository _groupRepository;

		protected EventMapperBase(IRepository<TModel> repository, IUserGroupRepository groupRepository) : base(repository)
		{
			_groupRepository = groupRepository;
		}

		protected override Guid GetIdFromMessage(TForm message)
		{
			return message.Id;
		}

		protected override void MapToModel(TForm form, TModel model)
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
		}
	}
}