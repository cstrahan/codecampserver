using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class TrackController : SaveController<Track, TrackForm>
	{
		private readonly ITrackRepository _repository;
		private readonly ITrackMapper _mapper;
		private readonly ISessionRepository _sessionsRepository;
	    private readonly ISecurityContext _securityContext;

	    public TrackController(ITrackRepository repository, ITrackMapper mapper, ISessionRepository sessionsRepository, ISecurityContext securityContext)
			: base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_sessionsRepository = sessionsRepository;
		    _securityContext = securityContext;
		}

		public ViewResult New(Conference conference)
		{
			var model = new TrackForm {ConferenceId = conference.Id, ConferenceKey = conference.Key};
			return View("Edit", model);
		}

		public ViewResult Index(Conference conference)
		{
			Track[] tracks = _repository.GetAllForConference(conference);
			return View(_mapper.Map(tracks));
		}

		public ViewResult Edit(Track track)
		{
            if(!_securityContext.HasPermissionsFor(track.Conference))
            {
                return NotAuthorizedView;
            }
			return View(_mapper.Map(track));
		}

		[ValidateModel(typeof (TrackForm))]
		public ActionResult Save([Bind(Prefix = "")] TrackForm trackForm, Conference conference, string urlreferrer)
		{
            if(!_securityContext.HasPermissionsFor(conference))
            {
                return NotAuthorizedView;
            }

			Func<Track, ActionResult> successRedirect = GetSuccessRedirect(conference, urlreferrer);

			return ProcessSave(trackForm, successRedirect);
		}

		private Func<Track, ActionResult> GetSuccessRedirect(Conference conference, string urlreferrer)
		{
			Func<Track, ActionResult> successRedirect =
				track => RedirectToAction<TrackController>(x => x.Index(null), new {conference = conference});

			if (!String.IsNullOrEmpty(urlreferrer))
			{
				successRedirect = track => Redirect(urlreferrer);
			}
			return successRedirect;
		}

		public ActionResult Delete(Track track)
		{
            if(!_securityContext.HasPermissionsFor(track.Conference))
            {
                return NotAuthorizedView;
            }

			if (_sessionsRepository.GetAllForTrack(track).Length == 0)
			{
				_repository.Delete(track);
			}
			else
			{
				TempData.Add("message", "Track cannot be deleted.");
			}
			return RedirectToAction<TrackController>(x => x.Index(null));
		}
	}
}