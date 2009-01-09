using System.Web.Mvc.Html;
using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class CheckboxInputBuilder : BaseInputCreator
	{
		public CheckboxInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override string CreateInputElementBase()
		{
			return InputBuilder.Helper.CheckBox(GetCompleteInputName(), InputBuilder.Attributes);
		}
	}
}