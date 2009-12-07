using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class InputBuilderPropertyConvention : DefaultProperyConvention
	{
		public override bool PropertyIsRequiredConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.AttributeExists<ShowAsRequiredAttribute>())
				return true;

			if (propertyInfo.AttributeExists<RequiredAttribute>())
				return true;

			return base.PropertyIsRequiredConvention(propertyInfo);
		}

		public override string LabelForPropertyConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.AttributeExists<LabelAttribute>())
				return propertyInfo.GetAttribute<LabelAttribute>().Label;

			return base.LabelForPropertyConvention(propertyInfo);
		}
	}
}