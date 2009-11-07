using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class UserController : ConventionController
	{
		private readonly IUserMapper _mapper;
		private readonly IUserRepository _repository;
		private readonly ISecurityContext _securityContext;

		public UserController(IUserRepository repository, IUserMapper mapper, ISecurityContext securityContext,
		                      IUserSession session)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
		}

		[HttpGet]
		public ViewResult Edit(User user)
		{
			if (!_securityContext.IsAdmin())
			{
				return NotAuthorizedView;
			}

			if (user == null)
			{
				return View(_mapper.Map(new User()));
			}

			UserInput input = _mapper.Map(user);
			return View(input);
		}

		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
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


		public ViewResult Index()
		{
			return View(_mapper.Map(_repository.GetAll()));
		}
	}
}