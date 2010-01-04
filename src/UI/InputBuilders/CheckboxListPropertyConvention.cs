using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.InputBuilders.SelectListProvision;
using MvcContrib.UI.InputBuilder.Conventions;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.UI.InputBuilders
{
	public class CheckboxListPropertyConvention : InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return propertyInfo.HasCustomAttribute<CheckboxListAttribute>();
		}

		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "CheckboxList";
		}

		public override PropertyViewModel CreateViewModel<T>()
		{
			return base.CreateViewModel<IEnumerable<SelectListItem>>();
		}

		public override object ValueFromModelPropertyConvention(PropertyInfo propertyInfo, object model, string parent)
		{
			var selectList = GetSelectList(propertyInfo);
			
			var values = (IEnumerable<PersistentObject>) propertyInfo.GetValue(model, null);

			if (values != null)
			{
				var selectedIds = values.Select(x => x.Id.ToString());
				SetSelectedValues(selectList, selectedIds);
			}

			return selectList;
		}

		private static void SetSelectedValues(IEnumerable<SelectListItem> selectList, IEnumerable<string> selectedIds)
		{
			foreach (var list in selectList)
			{
				if (selectedIds.Contains(list.Value))
					list.Selected = true;
			}
		}

		private static SelectList GetSelectList(PropertyInfo propertyInfo)
		{
			var provider = GetProvider(propertyInfo);

			return provider.Provide(null);
		}

		private static ISelectListProvider GetProvider(PropertyInfo propertyInfo)
		{
			var typeProviding = propertyInfo
				.GetAttribute<CheckboxListAttribute>()
				.SelectListProvider;

			return SelectListProviderFactory.GetDefault(typeProviding);
		}
	}
}