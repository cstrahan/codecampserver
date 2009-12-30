using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Infrastructure.Prince
{
    public class PrinceWrapper
    {
        public void AttachPrinceFilter(HttpContext httpContext)
        {
            var path = GetPrincePath(httpContext);
            var prince = new global::Prince(path);
            prince.SetBaseURL(httpContext.Request.Url.AbsoluteUri);
            prince.SetLog("prince.log");
            prince.SetInsecure(true);

            httpContext.Response.Filter = new PrinceFilter(prince, httpContext.Response.Filter);
        }

        private string GetPrincePath(HttpContext httpContext)
        {
            const string PRINCE_PATH = "PrincePathToExe";

            string setting = ConfigurationManager.AppSettings[PRINCE_PATH];
            if (setting == null)
                throw new Exception(string.Format("Required Configuration Setting {0} is missing", PRINCE_PATH));

            string path1 = httpContext.Request.PhysicalApplicationPath;
            string combine = Path.Combine(path1, setting);
            return Path.GetFullPath(combine);
        }
    }
}
