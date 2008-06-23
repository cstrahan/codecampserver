using System.Security;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using MvcContrib.Filters;
using IUserSession=CodeCampServer.Model.IUserSession;

namespace CodeCampServer.Website.Controllers
{
	public class LoginController : Controller
	{
		private readonly IPersonRepository _personRepository;
		private readonly IAuthenticator _authenticator;
		private readonly IUserSession _userSesssion;
		private readonly ICryptographer _cryptographer;

		public LoginController(IUserSession userSession, IPersonRepository personRepository,
		                       IAuthenticator authenticator, ICryptographer cryptographer)
			: base(userSession)
		{
			this._userSesssion = userSession;
			_cryptographer = cryptographer;
			_personRepository = personRepository;
			_authenticator = authenticator;
		}

		public ActionResult Index()
		{
			int numberOfUsers = getNumberOfUsers();
			ViewData["ShowFirstTimeRegisterLink"] = (numberOfUsers == 0);

			return View("loginform");
		}

		public ActionResult Process(string email, string password, string redirectUrl)
		{
			Person person = _personRepository.FindByEmail(email);
			if (person != null && _authenticator.VerifyAccount(person, password))
			{
				_authenticator.SignIn(person);

				if (redirectUrl != null)
					return new UrlRedirectResult(redirectUrl);

				return RedirectToAction("current", "conference");
			}

			//login failed
			_userSesssion.PushUserMessage(FlashMessage.MessageType.Error, "Invalid login");
			return RedirectToAction("index");
		}

		[PostOnly]
		public ActionResult CreateAdminAccount(string firstName, string lastName, string email, string password,
		                                       string passwordConfirm)
		{
			if (getNumberOfUsers() > 0)
			{
				throw new SecurityException(
					"This action is only valid when there are no registered users in the system.");
			}

			var task = new CreateAdminAccountTask(_personRepository, _cryptographer, firstName,
			                                      lastName, email, password, passwordConfirm);

			task.Execute();

			if (!task.Success)
			{
				_userSesssion.PushUserMessage(FlashMessage.MessageType.Error, task.ErrorMessage);
			}

			return RedirectToAction("index");
		}

		public ActionResult Logout()
		{
			_authenticator.SignOut();
			return RedirectToAction("index", "home");
		}

		private int getNumberOfUsers()
		{
			return _personRepository.GetNumberOfUsers();
		}
	}
}