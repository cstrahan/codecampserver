using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class TrackController : SaveController<Track, ITrackMessage>
	{
		private readonly ITrackRepository _trackRepository;

		private readonly ITrackUpdater _trackUpdater;

		public TrackController(ITrackRepository trackRepository, ITrackUpdater trackUpdater)
		{
			_trackRepository = trackRepository;
			_trackUpdater = trackUpdater;
		}

		public ViewResult New(Conference conference)
		{
			var model = new TrackForm {ConferenceId = conference.Id, ConferenceKey = conference.Key};
			return View("Edit", model);
		}

		[AutoMappedToModelFilter(typeof (Track[]), typeof (TrackForm[]))]
		public ViewResult Index(Conference conference)
		{
			Track[] tracks = _trackRepository.GetAllForConference(conference);
			ViewData.Add(tracks);
			return View();
		}

		[AutoMappedToModelFilter(typeof (Track), typeof (TrackForm))]
		public ViewResult Edit(Track track)
		{
			ViewData.Add(track);
			return View();
		}

		[ValidateModel(typeof (TrackForm))]
		public ActionResult Save([Bind(Prefix = "")] TrackForm trackForm)
		{
			return ProcessSave(trackForm, () => RedirectToAction<TrackController>(x => x.Index(null)));
		}

		public RedirectToRouteResult Delete(Track track)
		{
			_trackRepository.Delete(track);
			return RedirectToAction<TrackController>(x => x.Index(null));
		}

		protected override IModelUpdater<Track, ITrackMessage> GetUpdater()
		{
			return _trackUpdater;
		}
	}
}