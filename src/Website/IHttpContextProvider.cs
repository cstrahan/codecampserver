using System.Web;

namespace CodeCampServer.Website
{
	public interface IHttpContextProvider
	{
		HttpContextBase GetCurrentHttpContext();
		HttpSessionStateBase GetHttpSession();
	}
}