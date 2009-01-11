using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class SessionController : SaveController<Session, SessionForm>
	{
		private readonly ISessionRepository _repository;

		public SessionController(ISessionRepository repository, ISessionMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
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

		protected override IDictionary<string, string[]> GetFormValidationErrors(SessionForm form)
		{
			var result = new ValidationResult();
			if (SpeakerKeyAlreadyExists(form))
			{
				result.AddError<SessionForm>(x => x.Key, "This session key already exists");
			}
			return result.GetAllErrors();
		}

		private bool SpeakerKeyAlreadyExists(SessionForm message)
		{
			return _repository.GetByKey(message.Key) != null;
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