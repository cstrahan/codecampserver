using System;
using System.Reflection;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class DatePickerPropertyConvention : InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return typeof (DateTime).IsAssignableFrom(propertyInfo.PropertyType);
		}

		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "DatePicker";
		}

		public override PropertyViewModel CreateViewModel<T>()
		{
			return base.CreateViewModel<DateTime>();
		}
	}
}