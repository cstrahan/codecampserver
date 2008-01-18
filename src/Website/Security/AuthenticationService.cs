using System.Web.Security;
using CodeCampServer.Model;
using StructureMap;
using CodeCampServer.Model.Security;
using System.Web;
namespace CodeCampServer.Website.Security
{
	[Pluggable(Keys.DEFAULT)]
	public class AuthenticationService : IAuthenticationService
	{
		public void SetActiveUser(string username)
		{
			FormsAuthentication.SignOut();
			FormsAuthentication.SetAuthCookie(username, false);
		}

        public string GetActiveUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }
	}
}
