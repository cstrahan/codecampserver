using System.Web.Mvc.Html;

namespace CodeCampServer.UI.ViewPage.InputBuilders
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