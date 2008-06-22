using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;
using MvcContrib.Attributes;
using MvcContrib.Filters;
using IUserSession=CodeCampServer.Model.IUserSession;

namespace CodeCampServer.Website.Controllers
{
	public class ConferenceController : Controller
	{
		private readonly IClock _clock;
		private readonly IConferenceService _conferenceService;
		private readonly IUserSession _userSession;
		private readonly IConferenceRepository _conferenceRepository;

		public ConferenceController(IConferenceRepository conferenceRepository,
		                            IConferenceService conferenceService,
		                            IUserSession userSession,
		                            IClock clock) : base(userSession)
		{
			_conferenceRepository = conferenceRepository;
			_conferenceService = conferenceService;
			_userSession = userSession;
			_clock = clock;
		}

		[DefaultAction]
		public ActionResult Details(string conferenceKey)
		{
			Schedule schedule = getScheduledConference(conferenceKey);

			//if this conference is still hidden from the public, then only show it to administrators
			if (!schedule.Conference.PubliclyVisible && !_userSession.IsAdministrator)
				return RedirectToAction("current", "conference");

			ViewData.Add(schedule);

			return View();
		}

		public ActionResult KeyCheck(string conferenceKey)
		{
			bool result = _conferenceRepository.ConferenceKeyAvailable(conferenceKey);
			return new JsonResult(string.Format("{{{0}}}", result.ToString().ToLower()));
		}

		public ActionResult Current()
		{
			Conference conference = _conferenceService.GetCurrentConference();

			//if there are no conferences, then this is likely an admin setting the
			//site up for the first time
			if (conference == null)
				return RedirectToAction("index", "admin");

			return RedirectToAction("details", "conference", new {conferenceKey = conference.Key});
		}

		[AdminOnly]
		public ActionResult List()
		{
			Conference[] conferences = _conferenceRepository.GetAllConferences();
			ViewData.Add(conferences);
			return View();
		}

		public ActionResult PleaseRegister(string conferenceKey)
		{
			Schedule conference = getScheduledConference(conferenceKey);
			ViewData.Add(conference);
			return View("registerform");
		}

		public ActionResult Register(string conferenceKey, string firstName, string lastName, string email,
		                             string website,
		                             string comment, string password)
		{
			try
			{
				//register the attendee
				Conference conference = getConferenceByKey(conferenceKey);
				Person person = _conferenceService.RegisterAttendee(firstName, lastName, email, website, comment,
				                                                    conference, password);

				//sign them in
				ViewData.Add(person).Add(new Schedule(conference, _clock, null, null));
				return View("registerconfirm");
			}
			catch (Exception exc)
			{
				TempData[TempDataKeys.Error] = "An error occurred while registering your account.";
				Log.Error(this, "An error occcurred while registering a user", exc);
				return View("pleaseregister");
			}
		}

		public ActionResult ListAttendees(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			Conference conference = getConferenceByKey(conferenceKey);
			Person[] attendees = conference.GetAttendees();

			//TODO: implement paging for attendee listing
			//List<Person> pageOfAttendees = new List<Person>(attendees).GetRange(effectivePage * effectivePerPage, effectivePerPage);

			AttendeeListing[] listings = getListingsFromAttendees(attendees);

			ViewData
				.Add(new Schedule(conference, _clock, null, null))
				.Add(listings);

			return View();
		}

		[AdminOnly]
		public ActionResult New()
		{
			ViewData.Add(new Conference());
			return View("edit");
		}

		[AdminOnly]
		[PostOnly]
		public ActionResult Save(string conf_name, string conf_key, DateTime conf_start, DateTime? conf_end,
		                         string conf_desc)
		{
			if (_conferenceRepository.ConferenceExists(conf_name, conf_key))
			{
				TempData[TempDataKeys.Error] = "A conference has already been created with that name or key";
			}

			var conf = new Conference(conf_key, conf_name)
			           	{StartDate = conf_start, EndDate = conf_start, Description = conf_desc};

			try
			{
				_conferenceRepository.Save(conf);
				TempData[TempDataKeys.Message] = "The conference was created successfully.";
				return RedirectToAction("list");
			}
			catch (Exception exc)
			{
				Log.Error("Error saving conference.", exc);
				TempData[TempDataKeys.Error] = "There was an error saving the conference.  The error was: " + exc;
				ViewData.Add("conference", conf);
				return View("edit");
			}
		}

		[AdminOnly]
		public ActionResult Edit(string conferenceKey)
		{
			Conference conference = getConferenceByKey(conferenceKey);
			if (conference == null)
			{
				TempData[TempDataKeys.Error] = "Conference not found.";
				return RedirectToAction("current", "conference");
			}

			ViewData.Add(conference);
			return View("edit");
		}

		private Conference getConferenceByKey(string conferenceKey)
		{
			return _conferenceRepository.GetConferenceByKey(conferenceKey);
		}

		private Schedule getScheduledConference(string conferenceKey)
		{
			Conference conference = getConferenceByKey(conferenceKey);
			return new Schedule(conference, _clock, null, null);
		}

		private AttendeeListing[] getListingsFromAttendees(Person[] attendees)
		{
			var listings = new List<AttendeeListing>();
			foreach (Person attendee in attendees)
			{
				listings.Add(new AttendeeListing(attendee));
			}

			return listings.ToArray();
		}
	}
}