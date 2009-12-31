using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Binders.Keyed
{
	public class UserGroupModelBinder : KeyedModelBinder<UserGroup, IUserGroupRepository>
	{
		public UserGroupModelBinder(IUserGroupRepository repository) : base(repository) {}

		public override BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			ValueProviderResult value = GetRequestValue(bindingContext, bindingContext.ModelName, controllerContext);

			if (value == null || string.IsNullOrEmpty(value.AttemptedValue))
				return new BindResult(_repository.GetDefaultUserGroup(), value);

			UserGroup match = _repository.GetByKey(value.AttemptedValue);
			if (match != null)
				return new BindResult(match, value);

			return new BindResult(_repository.GetDefaultUserGroup(), value);
		}
	}
}