using System.Text;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class YesNoRadioInputBuilder : BaseInputBuilder
	{
		protected override bool UseSpanAsLabel
		{
			get { return true; }
		}

		public override bool IsSatisfiedBy(IInputSpecification specification)
		{
			return specification.PropertyInfo.PropertyType == typeof (bool?);
		}

		protected override string CreateInputElementBase()
		{
			var builder = new StringBuilder();

			builder.Append(CreateRadioInput("Yes", GetSelectedValue() == true, true));
			builder.Append(CreateRadioInput("No", GetSelectedValue() == false, false));

			return builder.ToString();
		}

		private string CreateRadioInput(string label, bool selected, bool value)
		{
			string checkedvalue = selected ? "checked=\"checked\"" : "";
			var classAttr = (string) InputSpecification.CustomAttributes.GetPropertyValue("class");
			return string.Format(
				"<input id=\"{4}\" type=\"radio\" value=\"{0}\" name=\"{1}\" {3} /><label class=\"nestedLabel {5}\" for=\"{4}\">{2}</label>",
				value, InputSpecification.InputName, label, checkedvalue,
				UINameHelper.BuildIdFrom(InputSpecification.Expression, value), classAttr);
		}

		private bool? GetSelectedValue()
		{
			object model = InputSpecification.Helper.ViewData.Model;
			if (model == null) return false;
			object value = model.GetPropertyValue(InputSpecification.PropertyInfo.Name);
			return (bool?) value;
		}
	}
}