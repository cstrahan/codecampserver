using System.Security.Authentication;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;

namespace CodeCampServer.UI.Helpers
{
	public class UserSession : IUserSession
	{
		private readonly IUserRepository _repository;

		public UserSession(IUserRepository repository)
		{
			_repository = repository;
		}

		public User GetCurrentUser()
		{
			IIdentity identity = HttpContext.Current.User.Identity;
			if (!identity.IsAuthenticated)
			{
				return null;
			}

			User currentUser = _repository.GetByUserName(identity.Name);
			blowUpIfEmployeeCannotLogin(currentUser);
			return currentUser;
		}

		public void LogIn(User user)
		{
			blowUpIfEmployeeCannotLogin(user);
			FormsAuthentication.RedirectFromLoginPage(user.Username, false);
		}

		public void LogOut()
		{
			FormsAuthentication.SignOut();
			HttpContext.Current.Response.Redirect("~/");
		}

		private static void blowUpIfEmployeeCannotLogin(User user)
		{
			if (user == null)
			{
				throw new InvalidCredentialException(
					"That user doesn't exist or is not valid.");
			}
		}
	}
}