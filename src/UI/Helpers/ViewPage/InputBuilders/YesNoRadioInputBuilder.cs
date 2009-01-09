using System.Text;
using CodeCampServer.Core;
using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	internal class YesNoRadioInputBuilder : BaseInputCreator
	{
		public YesNoRadioInputBuilder(InputBuilder builder)
			: base(builder)
		{
		}

		protected override bool UseSpanAsLabel
		{
			get { return true; }
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
			return string.Format(
				"<input id=\"{1}_{0}\" type=\"radio\" value=\"{0}\" name=\"{1}\" {3} /><label class=\"nestedLabel\" for=\"{1}_{0}\">{2}</label>",
				value, GetCompleteInputName(), label, checkedvalue);
		}

		private bool? GetSelectedValue()
		{
			var model = InputBuilder.Helper.ViewData.Model;
			if (model == null) return false;
			var value = model.GetPropertyValue(InputBuilder.PropertyInfo.Name);
			return (bool?)value;
		}
	}
}