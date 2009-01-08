using System.Text;
using System.Web.Mvc;
using CodeCampServer.UI.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class DisplayErrorMessages : IDisplayErrorMessages
	{
		private ModelStateDictionary _modelState;

		public ModelStateDictionary ModelState
		{
			set { _modelState = value; }
		}

		public string Display()
		{
			var errorHtml = new StringBuilder();
			if (_modelState.IsInvalid())
			{
				errorHtml.Append(
					@"<div class=""validationSummary"">
			            <p><strong>Please correct the following error(s):</strong></p><ul>");

				foreach (var error in _modelState)
				{
					foreach (ModelError modelError in error.Value.Errors)
					{
						errorHtml.AppendFormat(@"<li>{0}</li>", modelError.ErrorMessage);
					}
				}
				errorHtml.Append("</ul>");
				errorHtml.Append("</div>");
			}
			return errorHtml.ToString();
		}
	}
}