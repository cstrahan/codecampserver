using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Helpers.Binders
{
    public class UserGroupModelBinder:KeyedModelBinder<UserGroup,IUserGroupRepository>
    {
        public UserGroupModelBinder(IUserGroupRepository repository) : base(repository) {}

        public override object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            ValueProviderResult value = GetRequestValue(bindingContext, bindingContext.ModelName);


            if (value == null || string.IsNullOrEmpty(value.AttemptedValue)) return _repository.GetDefaultUserGroup();

            UserGroup match = _repository.GetByKey(value.AttemptedValue);
            if (match != null)
                return match;
            else
                return _repository.GetDefaultUserGroup();
        }
    }
}