using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Models;
using MvcContrib;
using MvcContrib.Attributes;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
	public class ConferenceController : BaseController
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
		public ActionResult Index(string conferenceKey)
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
		    return Json(result);
		}

		public ActionResult Current()
		{
			Conference conference = _conferenceService.GetCurrentConference();

			//if there are no conferences, then this is likely an admin setting the
			//site up for the first time
			if (conference == null)
				return RedirectToAction("index", "admin");

			return RedirectToAction("index", "conference", new {conferenceKey = conference.Key});
		}

		[Authorize(Roles="Administrator")]
		public ActionResult List()
		{
			Conference[] conferences = _conferenceRepository.GetAllConferences();
			ViewData.Add(conferences);
			return View();
		}

		public ActionResult PleaseRegister(string conferenceKey)
		{
			Schedule schedule = getScheduledConference(conferenceKey);
            ViewData.Add(schedule);

            if (!schedule.IsActive)
            {
                return View("cannotregister");
            }
		    
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

		public ActionResult ListAttendees(string conferenceKey)
		{			
			Conference conference = getConferenceByKey(conferenceKey);
			Person[] attendees = conference.GetAttendees();	

			AttendeeListing[] listings = getListingsFromAttendees(attendees);

			ViewData
				.Add(new Schedule(conference, _clock, null, null))
				.Add(listings);

			return View();
		}

        [Authorize(Roles = "Administrator")]
		public ActionResult New()
		{			
			return View("edit", new Conference());
		}

	    [Authorize(Roles = "Administrator")]
	    public ActionResult Edit(string conferenceKey)
	    {
	        var conference = getConferenceByKey(conferenceKey);
	        if (conference == null)
	        {
	            TempData[TempDataKeys.Error] = "Conference not found.";
	            return RedirectToAction("current", "conference");
	        }
	        
	        return View("edit", conference);
	    }

	    [Authorize(Roles = "Administrator")]
		[PostOnly]
		public ActionResult Save([ModelBinder(typeof(ConferenceModelBinder))] Conference conference)
		{
            if(conference == null)
            {
                TempData[TempDataKeys.Error] = "Conference not found.";
                return RedirectToAction("list");                
            }

			try
			{
				_conferenceRepository.Save(conference);
				TempData[TempDataKeys.Message] = "The conference was created successfully.";
				return RedirectToAction("list");
			}
			catch (Exception exc)
			{
				Log.Error("Error saving conference.", exc);
				TempData[TempDataKeys.Error] = "There was an error saving the conference.  The error was: " + exc;
				ViewData.Add("conference", conference);
				return View("edit");
			}
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