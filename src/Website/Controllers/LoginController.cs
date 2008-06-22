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
		private readonly IAuthenticationService _authenticationService;
		private readonly ICryptoUtil _cryptoUtil;

		public LoginController(IUserSession userSession, IPersonRepository personRepository,
		                       IAuthenticationService authenticationService, ICryptoUtil cryptoUtil)
			: base(userSession)
		{
			_cryptoUtil = cryptoUtil;
			_personRepository = personRepository;
			_authenticationService = authenticationService;
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
			if (person != null && _authenticationService.VerifyAccount(person, password))
			{
				_authenticationService.SignIn(person);

				if (redirectUrl != null)
					return new UrlRedirectResult(redirectUrl);

				return RedirectToAction("current", "conference");
			}

			//login failed
			TempData[TempDataKeys.Error] = "Invalid login";
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

			var task = new CreateAdminAccountTask(_personRepository, _cryptoUtil, firstName,
			                                      lastName, email, password, passwordConfirm);

			task.Execute();

			if (!task.Success)
			{
				TempData[TempDataKeys.Error] = task.ErrorMessage;
			}

			return RedirectToAction("index");
		}

		public ActionResult Logout()
		{
			_authenticationService.SignOut();
			return RedirectToAction("index", "home");
		}

		private int getNumberOfUsers()
		{
			return _personRepository.GetNumberOfUsers();
		}
	}
}