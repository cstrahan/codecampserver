using System.EnterpriseServices;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.ViewModels;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class TrackController : SaveController<Track, ITrackMessage>
	{
		private readonly ITrackRepository _trackRepository;
		private readonly ITrackUpdater _trackUpdater;

		public TrackController(ITrackRepository trackRepository, ITrackUpdater trackUpdater)
		{
			_trackRepository = trackRepository;
			_trackUpdater = trackUpdater;
		}

		[AutoMappedToModelFilter(typeof(Track[]), typeof(TrackViewModel[]))]
		public ActionResult Index(Conference conference)
		{
			Track[] tracks = _trackRepository.GetAllForConference(conference);
			ViewData.Add(tracks);
			return View();
		}

		protected override IModelUpdater<Track, ITrackMessage> Updater
		{
			get { return _trackUpdater; }
		}
	}
}