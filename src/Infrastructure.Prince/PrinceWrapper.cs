using System;
using System.Configuration;
using System.IO;
using System.Web;
using CodeCampServer.Core.Services.Impl;

namespace CodeCampServer.Infrastructure.Prince
{
    public class PrinceWrapper : IPrinceWrapper
    {
        public void AttachPrinceFilter(HttpContextBase httpContextBase)
        {
            var path = GetPrincePath(httpContextBase);
            var prince = new global::Prince(path);
            prince.SetBaseURL(httpContextBase.Request.Url.AbsoluteUri);
            prince.SetLog("prince.log");
            prince.SetInsecure(true);

            httpContextBase.Response.Filter = new PrinceFilter(prince, httpContextBase.Response.Filter);
        }

        private string GetPrincePath(HttpContextBase httpContext)
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