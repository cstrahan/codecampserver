using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc.Html;
using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class DateInputBuilder : BaseInputCreator
	{
		public DateInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override string CreateInputElementBase()
		{
			var attributes = MakeDictionary(InputBuilder.Attributes);

			if (attributes.ContainsKey("class"))
			{
				attributes["class"] = attributes["class"] + " date-pick";
			}
			else
			{
				attributes.Add("class", "date-pick");
			}

			return InputBuilder.Helper.TextBox(GetCompleteInputName(), null, attributes);
		}

		private static IDictionary<string, object> MakeDictionary(object withProperties)
		{
			var dic = new Dictionary<string, object>();
			var properties = TypeDescriptor.GetProperties(withProperties);
			foreach (PropertyDescriptor property in properties)
			{
				dic.Add(property.Name, property.GetValue(withProperties));
			}
			return dic;
		}
	}
}