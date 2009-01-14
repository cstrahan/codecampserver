using System.Text;
using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class DisplayErrorMessages : IDisplayErrorMessages
	{
		private TempDataDictionary _tempData;
		private ModelStateDictionary _modelState;

		public ModelStateDictionary ModelState
		{
			set { _modelState = value; }
		}

		public TempDataDictionary TempData
		{
			set { _tempData = value; }
		}

		public string Display()
		{
			var errorHtml = new StringBuilder();
			if (_modelState.IsInvalid() || _tempData.Count>0)
			{
				errorHtml.Append(
					string.Format(
						@"<div id=""{0}"" class=""{0}"">
			            <p><strong>Please correct the following error(s):</strong></p><ul>",
						"validationSummary"));

				foreach (var error in _modelState)
				{
					foreach (ModelError modelError in error.Value.Errors)
					{
						errorHtml.AppendFormat(@"<li>{0}</li>", modelError.ErrorMessage);
					}
				}

				foreach (var tempdata in _tempData)
				{
					errorHtml.AppendFormat(@"<li>{0}</li>", tempdata.Value);
				}
				
				errorHtml.Append("</ul>");
				errorHtml.Append("</div>");
			}
			return errorHtml.ToString();
		}
	}
}