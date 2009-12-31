using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.InputBuilders.SelectListProvision;
using MvcContrib.UI.InputBuilder.Conventions;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.UI.InputBuilders
{
	public class SelectListProvidedPropertyConvention : InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return propertyInfo.HasCustomAttribute<SelectListProvidedAttribute>();
		}

		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "DropDown";
		}

		public override PropertyViewModel CreateViewModel<T>()
		{
			return base.CreateViewModel<IEnumerable<SelectListItem>>();
		}

		public override object ValueFromModelPropertyConvention(PropertyInfo propertyInfo, object model, string parentName)
		{
			object selectedValue = propertyInfo.GetValue(model, null);

			Type typeProviding = propertyInfo
				.GetAttribute<SelectListProvidedAttribute>()
				.SelectListProvider;

			ISelectListProvider provider = SelectListProviderFactory.GetDefault(typeProviding);

			return provider.Provide(selectedValue);
		}
	}

	public class SelectListProviderFactory : AbstractFactoryBase<ISelectListProvider>
	{
		public static Func<Type, ISelectListProvider> GetDefault = type => DefaultUnconfiguredState();
	}
}