using System.Web;

namespace CodeCampServer.Website.Impl
{
	public class HttpContextProvider : IHttpContextProvider
	{
		public HttpContextBase GetCurrentHttpContext()
		{
			return new HttpContextWrapper2(HttpContext.Current);
		}

		public HttpSessionStateBase GetHttpSession()
		{
			return GetCurrentHttpContext().Session;
		}
	}
}