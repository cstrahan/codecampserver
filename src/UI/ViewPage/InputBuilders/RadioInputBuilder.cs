using System.Linq;
using System.Text;
using Cuc.Jcms.Core;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.ListManagement.Impl;

namespace Cuc.Jcms.UI.ViewPage.InputBuilders
{
	public class RadioInputBuilder : BaseInputCreator
	{
		public RadioInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override bool UseSpanAsLabel
		{
			get { return true; }
		}

		protected override string CreateInputElementBase()
		{
			var builder = new StringBuilder();

			foreach (
				var enumeration in
					EnumerationHelper.GetAll(InputBuilder.PropertyInfo.PropertyType).Cast<BetterEnumeration>().OrderBy(
						e => e.DisplayOrder))
			{
				string checkedvalue = (GetSelectedValue() == enumeration.DisplayName) ? "checked=\"checked\"" : "";

				builder.Append(
					string.Format(
						"<input id=\"{1}_{0}\" type=\"radio\" value=\"{0}\" name=\"{1}\" {3} /><label class=\"nestedLabel\" for=\"{1}_{0}\">{2}</label>",
						enumeration.Value, GetCompleteInputName(), enumeration.DisplayName, checkedvalue));
			}

			return builder.ToString();
		}

		private string GetSelectedValue()
		{
			var model = InputBuilder.Helper.ViewData.Model;
			if (model == null) return null;
			var value = model.GetPropertyValue(InputBuilder.PropertyInfo.Name);
			if (value != null) return ((Enumeration)value).DisplayName;
			return null;
		}
	}
}