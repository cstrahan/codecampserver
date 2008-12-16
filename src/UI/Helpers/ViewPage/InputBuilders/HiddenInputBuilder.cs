using System.Text;
using System.Web.Mvc.Html;

namespace CodeCampServer.UI.ViewPage.InputBuilders
{
	public class HiddenInputBuilder : BaseInputCreator
	{
		public HiddenInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override string CreateInputElementBase()
		{
			return InputBuilder.Helper.Hidden(GetCompleteInputName());
		}

		protected override void AppendLabel(StringBuilder output, string propertyName, string labelClass, bool isRequired)
		{
			return;
		}

		protected override void AppendCleaner(StringBuilder output)
		{
			return;
		}
	}
}