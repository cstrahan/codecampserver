using System;
using System.Web.Mvc;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.UI.Binders
{
	public class EnumerationModelBinder : IFilteredModelBinder
	{
		public BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			try
			{
				ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
				string enumerationValue = value == null ? string.Empty : value.AttemptedValue;

				if (enumerationValue == "")
				{
					return null;
				}

				Enumeration enumeration = GetEnumeration(bindingContext.ModelType, enumerationValue);
				return new BindResult(enumeration, value);
			}
			catch (Exception ex)
			{
				string message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                               bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
		}

		public bool ShouldBind(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return typeof (Enumeration).IsAssignableFrom(bindingContext.ModelType);
		}

		private static Enumeration GetEnumeration(Type enumerationType, string value)
		{
			int enumValue;
			bool success = int.TryParse(value, out enumValue);
			if (success)
			{
				return Enumeration.FromValueOrDefault(enumerationType, enumValue);
			}

			return Enumeration.FromDisplayNameOrDefault(enumerationType, value);
		}
	}
}