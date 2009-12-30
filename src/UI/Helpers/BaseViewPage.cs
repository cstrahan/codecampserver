using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Core;

namespace CodeCampServer.UI.Helpers
{
    public class BaseViewPage<TModel> : ViewPage<TModel>
    {
        protected override void OnPreInit(EventArgs e)
        {
            if (Request.Params["ViewAsPDF"] != null)
            {
                Response.AddHeader("Content-Type", "binary/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=Report.pdf;");
            }

            base.OnPreInit(e);
        }
    }

    public class ReportBaseViewPage<TModel> : ViewPage<TModel>
    {
        protected override void OnPreInit(EventArgs e)
        {
            if (Request.Params["ViewAsPDF"] != null)
            {
                Response.Clear();

                var princeReportWrapper = PrinceReportWrapperFactory.GetDefault();
                princeReportWrapper.AttachPrinceFilter(new HttpContextWrapper(HttpContext.Current));
                
                Response.AddHeader("Content-Type", "binary/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=Report.pdf;");
            } 
            base.OnPreInit(e);
        }
    }
}