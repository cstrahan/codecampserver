using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class ScheduleController : SmartController
	{
		private IScheduleMapper _mapper;

		public ScheduleController(IScheduleMapper mapper)
		{
			_mapper = mapper;
		}

		public ActionResult Index(Conference conference)
		{
			ScheduleForm scheduleForm = _mapper.Map(conference);
			return View(scheduleForm);
		}
	}
}