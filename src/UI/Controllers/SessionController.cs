using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Filters;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class SessionController : SaveController<Session, ISessionMessage>
	{
		private readonly ISessionUpdater _updater;
		private readonly ISessionRepository _repository;

		public SessionController(ISessionRepository repository, ISessionUpdater updater)
		{
			_updater = updater;
			_repository = repository;
		}

		protected override IModelUpdater<Session, ISessionMessage> GetUpdater()
		{
			return _updater;
		}

		public ViewResult New()
		{
			return View("Edit", new SessionForm());
		}

		[AutoMappedToModelFilter(typeof (Session[]), typeof (SessionForm[]))]
		public ViewResult Index(Conference conference)
		{
			Session[] sessions = _repository.GetAllForConference(conference);
			ViewData.Add(sessions);
			return View();
		}

		[AutoMappedToModelFilter(typeof (Session), typeof (Session))]
		public ViewResult Edit(Session session)
		{
			ViewData.Add(session);
			return View();
		}

		[ValidateModel(typeof (SessionForm))]
		public ActionResult Save([Bind(Prefix = "")] SessionForm form)
		{
			return ProcessSave(form, () => CreateRedirect(form.Conference));
		}

		public RedirectToRouteResult Delete(Session session)
		{
			_repository.Delete(session);
			return CreateRedirect(session.Conference);
		}

		private RedirectToRouteResult CreateRedirect(Conference conference)
		{
			return RedirectToAction<SessionController>(x => x.Index(null), new {conference = conference.Key});
		}
	}
}