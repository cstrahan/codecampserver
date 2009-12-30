using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.UI.InputBuilders
{
	public class UserPickerPropertyConvention : InputBuilderPropertyConvention
	{
		private readonly IUserRepository _userRepository;

		public UserPickerPropertyConvention(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return typeof (IEnumerable<UserSelectorInput>).IsAssignableFrom(propertyInfo.PropertyType);
		}

		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "ListBox";
		}

		public override PropertyViewModel CreateViewModel<T>()
		{
			return base.CreateViewModel<IEnumerable<SelectListItem>>();
		}

		public override object ValueFromModelPropertyConvention(PropertyInfo propertyInfo, object model,string parentName)
		{
			var value = propertyInfo.GetValue(model, null) as IEnumerable<UserSelectorInput>;
			var items = new List<SelectListItem>();

			foreach (User user in _userRepository.GetAll())
			{
				bool isChecked = value != null && (value).Where(form => form.Id == user.Id).Count() > 0;
				items.Add(new SelectListItem {Selected = isChecked, Text = user.Name, Value = user.Id.ToString()});
			}
			return items;
		}
	}
}