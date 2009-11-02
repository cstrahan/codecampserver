using System;
using System.Reflection;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;
using System.Linq;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class InputBuilderPropertyConventions : DefaultProperyConvention
	{
		public override string PartialNameConvention(PropertyInfo propertyInfo)
		{
			if (propertyInfo.Name.ToLower().Contains("password"))
				return "Password";
			if (propertyInfo.AttributeExists<MultilineAttribute>())
				return "MultilineText";
			if (typeof(Guid?).IsAssignableFrom(propertyInfo.PropertyType))
				return "Guid";
			return base.PartialNameConvention(propertyInfo);
		}

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