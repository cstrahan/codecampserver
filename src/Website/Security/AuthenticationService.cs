using System.Web.Security;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Security;
using System.Web;
namespace CodeCampServer.Website.Security
{
	public class AuthenticationService : IAuthenticationService
	{
	    private readonly IClock _clock;
	    private readonly HttpContextBase _httpContext;

	    public AuthenticationService() :this(new SystemClock(), new HttpContextWrapper2(HttpContext.Current))
	    {	        
	    }

	    public AuthenticationService(IClock clock, HttpContextBase httpContext)
	    {
	        _clock = clock;
	        _httpContext = httpContext;
	    }

	    public void SignIn(Person user)
		{
			FormsAuthentication.SignOut();			

		    var issued = _clock.GetCurrentTime();
		    var expires = issued.AddMinutes(30);
		    var roles = user.IsAdministrator ? "Administrator" : "User";

		    var ticket = new FormsAuthenticationTicket(1, user.GetName(), issued, expires, false, roles);
	        var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(ticket.Name, encryptedTicket) {Expires = ticket.Expiration};
	        
            _httpContext.Response.Cookies.Add(authCookie);
		}

        public string GetActiveUserName()
        {
            return _httpContext.User.Identity.Name;
        }
	}
}
