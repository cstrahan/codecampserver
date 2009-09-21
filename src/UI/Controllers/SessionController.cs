using System;
using System.Collections.Generic;
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
	public class SessionController : SaveController<Session, SessionForm>
	{
		private readonly ISessionMapper _mapper;
		private readonly ISessionRepository _repository;
		private readonly ISecurityContext _securityContext;

		public SessionController(ISessionRepository repository, ISessionMapper mapper, ISecurityContext securityContext)
			: base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
		}

		public ViewResult New(Conference conference)
		{
			if (!_securityContext.HasPermissionsFor(conference))
			{
				return NotAuthorizedView;
			}

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
			if (!_securityContext.HasPermissionsFor(session))
			{
				return NotAuthorizedView;
			}

			return View(_mapper.Map(session));
		}

		[ValidateModel(typeof (SessionForm))]
		public ActionResult Save([Bind(Prefix = "")] SessionForm form, string urlreferrer, Conference conference)
		{
			if (!_securityContext.HasPermissionsFor(conference))
			{
				return NotAuthorizedView;
			}

			form.Conference = conference;
			Func<Session, ActionResult> successRedirect = GetSuccessRedirect(urlreferrer, conference);
			return ProcessSave(form, successRedirect);
		}

		private Func<Session, ActionResult> GetSuccessRedirect(string urlreferrer, Conference conference)
		{
			Func<Session, ActionResult> successRedirect = session => RedirectToIndex(conference);
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

		public ActionResult Delete(Session session)
		{
			if (!_securityContext.HasPermissionsFor(session))
			{
				return NotAuthorizedView;
			}
			_repository.Delete(session);
			return RedirectToIndex(session.Conference);
		}

		private RedirectToRouteResult RedirectToIndex(Conference conference)
		{
			return RedirectToAction<SessionController>(x => x.Index(null), new {conference = conference.Key});
		}
	}
}