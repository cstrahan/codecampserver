using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
//	[RequiresConferenceFilter]
	public class NewsletterController : SmartController
	{
		private readonly IConferenceRepository _conferenceRepository;

		public NewsletterController(IConferenceRepository attendeeRepository)
		{
			_conferenceRepository = attendeeRepository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Save(AttendeeForm attendeeForm)
		{
			Conference conference = _conferenceRepository.GetById(attendeeForm.ConferenceID);
			Attendee existingEntry = conference.GetAttendees().Where(a => a.EmailAddress == attendeeForm.EmailAddress).SingleOrDefault();
			if (existingEntry == null)
			{
				conference.AddAttendee(new Attendee {EmailAddress = attendeeForm.EmailAddress, FirstName = attendeeForm.FirstName});
				_conferenceRepository.Save(conference);
				TempData.Add("message", "You have subscribed to the newsletter");
			}
			else
			{
				TempData.Add("message", "That email is already subscribed to the newsletter");
			}
			return View("index");
		}
	}
}