using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class ScheduleController : SmartController
	{
		private readonly IScheduleMapper _mapper;

		public ScheduleController(IScheduleMapper mapper)
		{
			_mapper = mapper;
		}

		public ViewResult Index(Conference conference)
		{
			//leverage the schedule mapper to map the conference
			//into our presentation model
			ScheduleForm[] scheduleForms = _mapper.Map(conference);
			return View(scheduleForms);
		}
	}
}