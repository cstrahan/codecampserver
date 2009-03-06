using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc.Html;
using Castle.Components.Validator;
using CodeCampServer.Core.Common;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class DateInputBuilder : BaseInputBuilder
	{
		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return (specification.PropertyInfo.HasCustomAttribute<ValidateDateAttribute>()) ||
			       (specification.PropertyInfo.HasCustomAttribute<ValidateDateTimeAttribute>());
		}

		protected override string CreateInputElementBase()
		{
			IDictionary<string, object> attributes = InputSpecification.CustomAttributes;

			if (attributes.ContainsKey("class"))
			{
				attributes["class"] = attributes["class"] + " date-pick";
			}
			else
			{
				attributes.Add("class", "date-pick");
			}

			return InputSpecification.Helper.TextBox(InputSpecification.InputName, null, attributes);
		}
	}
}