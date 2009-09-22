using System.Linq;
using System.Text;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class RadioInputBuilder : BaseInputBuilder
	{
		protected override bool UseSpanAsLabel
		{
			get { return true; }
		}

		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return specification.PropertyInfo.PropertyType == typeof (YesNo);
		}

		protected override string CreateInputElementBase()
		{
			var builder = new StringBuilder();

			foreach (
				var enumeration in
					EnumerationHelper.GetAll(InputSpecification.PropertyInfo.PropertyType).Cast<OrderedEnumeration>().OrderBy(
						e => e.DisplayOrder))
			{
				string checkedvalue = (GetSelectedValue() == enumeration.DisplayName) ? "checked=\"checked\"" : "";

				builder.Append(
					string.Format(
						"<input id=\"{4}\" type=\"radio\" value=\"{0}\" name=\"{1}\" {3} /><label class=\"nestedLabel\" for=\"{4}\">{2}</label>",
						enumeration.Value, InputSpecification.InputName, enumeration.DisplayName, checkedvalue, UINameHelper.BuildIdFrom(InputSpecification.Expression, enumeration)));
			}

			return builder.ToString();
		}

		private string GetSelectedValue()
		{
			var model = InputSpecification.Helper.ViewData.Model;
			if (model == null) return null;
			var value = model.GetPropertyValue(InputSpecification.PropertyInfo.Name);
			if (value != null) return ((Enumeration)value).DisplayName;
			return null;
		}
	}
}