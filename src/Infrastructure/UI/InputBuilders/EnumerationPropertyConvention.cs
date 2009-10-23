using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model.Enumerations;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class EnumerationPropertyConvention:InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return typeof(Enumeration).IsAssignableFrom(propertyInfo.PropertyType);
		}
		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "Enum";
		}
		public override PropertyViewModel CreateViewModel<T>()
		{
			return base.CreateViewModel<IEnumerable<SelectListItem>>();
		}
		public override object ValueFromModelPropertyConvention(PropertyInfo propertyInfo, object model)
		{
			var value = propertyInfo.GetValue(model, null) as Enumeration;
			var items = new List<SelectListItem>();

			foreach (Enumeration level in Enumeration.GetAll(propertyInfo.PropertyType))
			{
				bool isChecked = value != null && value == level;
				items.Add(new SelectListItem { Selected = isChecked, Text = level.DisplayName, Value = level.Value.ToString() });
			}
			return items;
		}
	}
}