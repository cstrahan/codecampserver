using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.UI.Controllers
{
    public class NewsletterController : Controller
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
            var conference = _conferenceRepository.GetById(attendeeForm.ConferenceID);
            var existingEntry = conference.Attendees.Where(a => a.EmailAddress == attendeeForm.Email).SingleOrDefault();
            if (existingEntry == null)
            {
                conference.AddAttendee( new Attendee {EmailAddress = attendeeForm.Email, FirstName = attendeeForm.Name});
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