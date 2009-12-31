using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace CodeCampServer.UI.Helpers
{
    public class PrinceModule : IHttpModule 
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        public void Dispose() {}

        private void context_EndRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            if (httpApplication.Request.Params["ViewAsPDF"] != null)
            {
                httpApplication.Response.AddHeader("Content-Type", "binary/octet-stream");
                httpApplication.Response.AddHeader("Content-Disposition", "attachment; filename=Report.pdf;");
            }
        }
        private void context_BeginRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;

            if (httpApplication.Request.Params["ViewAsPDF"] != null)
            {
                httpApplication.Response.Clear();

                AttachPrinceFilter(httpApplication);
            }
        }

        private static void AttachPrinceFilter(HttpApplication httpApplication)
        {
            var path = GetPrincePath(httpApplication);
            var prince = new Prince(path);
            prince.SetBaseURL(httpApplication.Request.Url.AbsoluteUri);
            prince.SetLog("prince.log");
            prince.SetInsecure(true);

            httpApplication.Response.Filter = new PrinceFilter(prince, httpApplication.Response.Filter);
        }

        private static string GetPrincePath(HttpApplication httpApplication)
        {
            const string PRINCE_PATH = "PrincePathToExe";

            string setting = ConfigurationManager.AppSettings[PRINCE_PATH];
            if (setting == null)
                throw new Exception(string.Format("Required Configuration Setting {0} is missing", PRINCE_PATH));

            string path1 = httpApplication.Request.PhysicalApplicationPath;
            string combine = Path.Combine(path1, setting);
            return Path.GetFullPath(combine);
        }
    }
}