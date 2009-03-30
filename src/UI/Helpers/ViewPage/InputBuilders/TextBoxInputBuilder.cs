using System;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class TextBoxInputBuilder : BaseInputBuilder
	{
		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return specification.PropertyInfo.PropertyType == typeof (string);
		}

		protected override string CreateInputElementBase()
		{
			var customAttributes = InputSpecification.CustomAttributes;
			if (customAttributes != null && customAttributes.ContainsKey("rows"))
			{
				return InputSpecification.Helper.TextArea(InputSpecification.InputName, GetValue().ToNullSafeString(),
				                                          customAttributes);
			}

			if (InputSpecification.PropertyInfo.Name.EndsWith("password", StringComparison.InvariantCultureIgnoreCase))
				return InputSpecification.Helper.Password(InputSpecification.InputName, GetValue(), customAttributes);

			return InputSpecification.Helper.TextBox(InputSpecification.InputName, GetValue(), customAttributes);
		}
	}
}