using System;
using System.Web.Mvc;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Infrastructure.UI.Binders
{
	public class EnumerationModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			try
			{
				string enumerationValue = GetAttemptedValue(bindingContext);

				if (enumerationValue == "")
				{
					return null;
				}

				Enumeration enumeration = GetEnumeration(bindingContext.ModelType, enumerationValue);
				return enumeration;
			}
			catch (Exception ex)
			{
				string message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                               bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
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

		private static string GetAttemptedValue(ModelBindingContext bindingContext)
		{
			ValueProviderResult value = bindingContext.ValueProvider[bindingContext.ModelName];
			return value == null ? string.Empty : value.AttemptedValue;
		}
	}
}