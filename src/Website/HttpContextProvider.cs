using System.Web;

namespace CodeCampServer.Website
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContextBase GetCurrentHttpContext()
        {
            return new HttpContextWrapper2(HttpContext.Current);
        }
    }
}
