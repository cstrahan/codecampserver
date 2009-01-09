using System.Web.Mvc.Html;
using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class TextBoxInputBuilder : BaseInputCreator
	{
		public TextBoxInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override string CreateInputElementBase()
		{
			return InputBuilder.Helper.TextBox(GetCompleteInputName(), null, InputBuilder.Attributes);
		}
	}
}