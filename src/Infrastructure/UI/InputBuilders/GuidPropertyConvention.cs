using System;
using System.Reflection;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class GuidPropertyConvention : InputBuilderPropertyConvention
	{
		public override bool CanHandle(PropertyInfo propertyInfo)
		{
			return typeof (Guid).IsAssignableFrom(propertyInfo.PropertyType);
		}

		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			return "Guid";
		}

		public override PropertyViewModel CreateViewModel<T>()
		{
			return base.CreateViewModel<Guid>();
		}
		public override string Layout()
		{
			return "HiddenField";
		}
	}
}