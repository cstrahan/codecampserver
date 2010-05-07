using System.Web.Mvc;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Display;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class HeartbeatController : ConventionController
	{
		private readonly IHeartbeatRepository _repository;
		private readonly IHeartbeatChecker _checker;

		public HeartbeatController(IHeartbeatRepository repository, IHeartbeatChecker checker)
		{
			_repository = repository;
			_checker = checker;
		}

		[HttpPost]
		public ActionResult Edit(CreateHeartbeatInput input)
		{
			return Command<CreateHeartbeatInput, object>(input,
			                                       r => RedirectToAction<HeartbeatController>(c => c.Index()),
			                                       i => View(input)
				);
		}

		[HttpGet]
		public ViewResult Edit()
		{
			return View(new CreateHeartbeatInput());
		}


		[HttpGet]
		public ViewResult Index()
		{
			return AutoMappedView<HeartbeatDisplay[]>(_repository.GetTop());
		}

		[HttpGet]
		public ViewResult Check(int timeout)
		{
			var result = _checker.CheckHeartbeat(timeout);
			return View(new HeartbeatCheckDisplay{Message = result});
		}
	}
}