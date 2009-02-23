using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class SessionController : SaveController<Session, SessionForm>
	{
		private readonly ISessionMapper _mapper;
		private readonly ISessionRepository _repository;

		public SessionController(ISessionRepository repository, ISessionMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public ViewResult New()
		{
			return View("Edit", new SessionForm());
		}

		public ViewResult Index(Session session)
		{
			return View(_mapper.Map(session));
		}

		public ViewResult List(Conference conference)
		{
			Session[] sessions = _repository.GetAllForConference(conference);
			return View(_mapper.Map(sessions));
		}

		public ViewResult Edit(Session session)
		{
			return View(_mapper.Map(session));
		}

		[ValidateModel(typeof (SessionForm))]
		public ActionResult Save([Bind(Prefix = "")] SessionForm form, string urlreferrer)
		{
			Func<Session, ActionResult> successRedirect = GetSuccessRedirect(form, urlreferrer);
			return ProcessSave(form, successRedirect);
		}

		private Func<Session, ActionResult> GetSuccessRedirect(SessionForm form, string urlreferrer)
		{
			Func<Session, ActionResult> successRedirect = session => RedirectToIndex(form.Conference);
			if (!String.IsNullOrEmpty(urlreferrer))
			{
				successRedirect = session => Redirect(urlreferrer);
			}
			return successRedirect;
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(SessionForm form)
		{
			var result = new ValidationResult();
			if (KeyAlreadyExists(form))
			{
				result.AddError<SessionForm>(x => x.Key, "This session key already exists");
			}
			return result.GetAllErrors();
		}

		private bool KeyAlreadyExists(SessionForm message)
		{
			Session session = _repository.GetByKey(message.Key);
			return session != null && session.Id != message.Id;
		}

		public RedirectToRouteResult Delete(Session session)
		{
			_repository.Delete(session);
			return RedirectToIndex(session.Conference);
		}

		private RedirectToRouteResult RedirectToIndex(Conference conference)
		{
			return RedirectToAction<SessionController>(x => x.Index(null), new {conference = conference.Key});
		}
	}
}