using System.Web.Security;
using StructureMap;
using CodeCampServer.Model.Security;
namespace CodeCampServer.Website.Security
{
	[Pluggable("Default")]
	public class AuthenticationService : IAuthenticationService
	{
		public void SetActiveUser(string username)
		{
			FormsAuthentication.SignOut();
			FormsAuthentication.SetAuthCookie(username, false);
		}
	}
}
