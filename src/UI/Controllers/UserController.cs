using System.Web.Mvc;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;
//using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UI.Controllers
{
	public class UserController : ConventionController
	{
		private readonly IUserRepository _repository;
		private readonly ISecurityContext _securityContext;

		public UserController(IUserRepository repository, ISecurityContext securityContext)
		{
			_repository = repository;
			_securityContext = securityContext;
		}

		[HttpGet]
		public ViewResult Edit(User entity)
		{
			if (!_securityContext.IsAdmin())
			{
				return NotAuthorizedView;
			}
			return AutoMappedView<UserInput>(entity ?? new User());
		}

		[HttpPost]
		[Authorize]
		public ActionResult Edit(UserInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.Id))
			{
				return View(ViewPages.NotAuthorized);
			}

			return Command<UserInput, object>(input,
			                                  r => RedirectToAction<HomeController>(c => c.Index(null)),
			                                  i => View(input)
				);
		}

		[HttpGet]
		public ViewResult Display(User entity)
		{
			return AutoMappedView<UserInput>(entity);
		}

		public ViewResult Index()
		{
			return AutoMappedView<UserInput[]>(_repository.GetAll());
		}
	}
}