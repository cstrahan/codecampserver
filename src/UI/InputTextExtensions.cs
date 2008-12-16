using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CodeCampServer.UI
{
	public static class InputTextExtensions
	{
		public static string RequiredTextBox(this HtmlHelper helper, string name)
		{
			return helper.RequiredTextBox(name, null);
		}

		public static string RequiredTextBox(this HtmlHelper helper, string name, object value)
		{
			return helper.TextBox(name, value, new {@class = "required-field"});
		}

		public static string DateTextBox(this HtmlHelper helper, string name)
		{
			return helper.DateTextBox(name, null);
		}

		public static string DateTextBox(this HtmlHelper helper, string name, object value)
		{
			return helper.TextBox(name, value, new {@class = "date-input"});
		}
	}
}