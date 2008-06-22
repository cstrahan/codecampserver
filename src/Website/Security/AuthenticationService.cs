using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Security
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IClock _clock;
		private readonly IHttpContextProvider _httpContextProvider;
		private readonly ICryptoUtil _cryptoUtil;

		public AuthenticationService(IClock clock, IHttpContextProvider httpContextProvider, ICryptoUtil cryptoUtil)
		{
			_clock = clock;
			_cryptoUtil = cryptoUtil;
			_httpContextProvider = httpContextProvider;
		}

		public void SignIn(Person user)
		{
			FormsAuthentication.SignOut();

			DateTime issued = _clock.GetCurrentTime();
			DateTime expires = issued.AddMinutes(30);
			string roles = user.IsAdministrator ? "Administrator" : "User";

			var ticket = new FormsAuthenticationTicket(1, user.GetName(), issued, expires, false, roles);
			string encryptedTicket = FormsAuthentication.Encrypt(ticket);
			var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
			                 	{Expires = ticket.Expiration};

			_httpContextProvider.GetCurrentHttpContext().Response.Cookies.Add(authCookie);
		}

		public IIdentity GetActiveIdentity()
		{
			return _httpContextProvider.GetCurrentHttpContext().User.Identity;
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public bool VerifyAccount(Person person, string password)
		{
			string passwordHash = _cryptoUtil.HashPassword(password, person.PasswordSalt);
			return passwordHash == person.Password;
		}
	}
}