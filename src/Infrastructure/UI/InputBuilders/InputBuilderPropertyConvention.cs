using System.Reflection;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class InputBuilderPropertyConvention : DefaultProperyConvention
	{
		public override bool PropertyIsRequiredConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.AttributeExists<ShowAsRequiredAttribute>())
				return true;

			if (propertyInfo.AttributeExists<ValidateNonEmptyAttribute>())
				return true;

			if (propertyInfo.AttributeExists<RequiredAttribute>())
				return true;

			return base.PropertyIsRequiredConvention(propertyInfo);
		}

		public override string LabelForPropertyConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.AttributeExists<LabelAttribute>())
				return propertyInfo.GetAttribute<LabelAttribute>().Value;

			if (propertyInfo.AttributeExists<RequiredAttribute>())
				return propertyInfo.GetAttribute<RequiredAttribute>().Label;

			return base.LabelForPropertyConvention(propertyInfo);
		}
	}
}