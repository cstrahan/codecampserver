using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model.Enumerations;
using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.UI.Helpers.Binders
{
	public class EnumerationModelBinder : IModelBinder
	{
		public ModelBinderResult BindModel(ModelBindingContext bindingContext)
		{
			try
			{
				string enumerationValue = GetAttemptedValue(bindingContext);

				if (enumerationValue == "")
				{
					return new ModelBinderResult(null);
				}

				Enumeration enumeration = GetEnumeration(bindingContext.ModelType, enumerationValue);
				return new ModelBinderResult(enumeration);
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
				return EnumerationHelper.FromValueOrDefault(enumerationType, enumValue);
			}

			return EnumerationHelper.FromDisplayNameOrDefault(enumerationType, value);
		}

		private static string GetAttemptedValue(ModelBindingContext bindingContext)
		{
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			return value == null ? string.Empty : value.AttemptedValue;
		}
	}
}