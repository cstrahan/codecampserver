using System.Web.Mvc.Html;

namespace CodeCampServer.UI.ViewPage.InputBuilders
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