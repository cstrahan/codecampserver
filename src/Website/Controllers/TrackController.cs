using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;
using IUserSession=CodeCampServer.Model.IUserSession;

namespace CodeCampServer.Website.Controllers
{
	public class TrackController : Controller
	{
		private readonly ITrackRepository _trackRepository;
		private readonly IConferenceRepository _conferenceRepository;
		private readonly IClock _clock;

		public TrackController(ITrackRepository trackRepository, IConferenceRepository conferenceRepository, IClock clock,
		                       IUserSession userSession)
			: base(userSession)
		{
			_trackRepository = trackRepository;
			_conferenceRepository = conferenceRepository;
			_clock = clock;
		}

		public ActionResult List(string conferenceKey)
		{
			Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
			Track[] tracks = _trackRepository.GetTracksFor(conference);
			ViewData.Add(new Schedule(conference, _clock, null, _trackRepository));
			ViewData.Add(tracks);

			return View();
		}
	}
}