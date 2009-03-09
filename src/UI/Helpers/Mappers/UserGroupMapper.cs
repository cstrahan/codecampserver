using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserGroupMapper : AutoFormMapper<UserGroup, UserGroupForm>, IUserGroupMapper
	{
		public UserGroupMapper(IUserGroupRepository repository) : base(repository)
		{
		}

		protected override Guid GetIdFromMessage(UserGroupForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(UserGroupForm form, UserGroup model)
		{
			model.Key = form.Key;
			model.Id = form.Id;
			model.Name = form.Name;
		}
	}
}
