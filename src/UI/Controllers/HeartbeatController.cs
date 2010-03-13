using System.Web.Mvc;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Display;

namespace CodeCampServer.UI.Controllers
{
	public class HeartbeatController : ConventionController
	{
		private readonly IHeartbeatRepository _repository;

		public HeartbeatController(IHeartbeatRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public ViewResult Index()
		{
			return AutoMappedView<HeartbeatDisplay[]>(_repository.GetAll());
		}
	}
}