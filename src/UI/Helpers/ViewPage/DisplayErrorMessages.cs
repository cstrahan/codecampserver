using System.Text;
using System.Web.Mvc;

namespace CodeCampServer.UI.ViewPage
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
                errorHtml.Append(@"<div class=""validationSummary"">
			            <p><strong>Please correct the following error(s):</strong></p><ul>");

                foreach (var error in _modelState)
                {
                    foreach (var modelError in error.Value.Errors)
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