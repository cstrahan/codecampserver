using System.Web;

namespace CodeCampServer.Website
{
    public class HttpContextFactory
    {
        public HttpContextBase GetCurrentHttpContext()
        {
            return new HttpContextWrapper2(HttpContext.Current);
        }
    }
}
