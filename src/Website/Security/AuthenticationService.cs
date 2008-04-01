using System.Web.Security;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using System.Web;
namespace CodeCampServer.Website.Security
{
	public class AuthenticationService : IAuthenticationService
	{
	    private readonly IClock _clock;
	    private readonly IHttpContextProvider _httpContextProvider;
	    
	    public AuthenticationService(IClock clock, IHttpContextProvider httpContextProvider)
	    {
	        _clock = clock;
	        _httpContextProvider = httpContextProvider;	        
	    }

	    public void SignIn(Person user)
		{
			FormsAuthentication.SignOut();			

		    var issued = _clock.GetCurrentTime();
		    var expires = issued.AddMinutes(30);
		    var roles = user.IsAdministrator ? "Administrator" : "User";           

		    var ticket = new FormsAuthenticationTicket(1, user.GetName(), issued, expires, false, roles);
	        var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) 
                { Expires = ticket.Expiration };

            _httpContextProvider.GetCurrentHttpContext().Response.Cookies.Add(authCookie);
		}

        public string GetActiveUserName()
        {
            return _httpContextProvider.GetCurrentHttpContext().User.Identity.Name;
        }
	}
}
