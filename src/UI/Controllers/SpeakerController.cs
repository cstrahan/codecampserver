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
	[RequiresConferenceFilterAttribute]
	public class SpeakerController : SaveController<Speaker, SpeakerForm>
	{
		private readonly ISpeakerRepository _repository;
		private readonly ISpeakerMapper _mapper;
		private readonly ISessionRepository _sessionsRepository;
	    private readonly ISecurityContext _securityContext;
        

	    public SpeakerController(ISpeakerRepository repository, ISpeakerMapper mapper, ISessionRepository sessionsRepository, ISecurityContext securityContext) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_sessionsRepository = sessionsRepository;
		    _securityContext = securityContext;
		}

		
		public ActionResult Index(Speaker speaker)
		{
			return View(_mapper.Map(speaker));
		}

		public ActionResult List(Conference conference)
		{
			var speakers = _repository.GetAllForConference(conference);
			return View(_mapper.Map(speakers));
		}

		[RequireAuthenticationFilter]
		public ActionResult Edit(Speaker speaker,Conference conference)
		{
			if (speaker == null)
			{
				TempData.Add("message", "Speaker has been deleted.");
				return RedirectToAction<SpeakerController>(c => c.List(conference));
			}
            if(_securityContext.HasPermissionsFor(speaker))
            {
                return View(_mapper.Map(speaker));
            }

            return NotAuthorizedView;            
		}

		[RequireAuthenticationFilter]
		public ActionResult Save([Bind(Prefix = "")] SpeakerForm form,Conference conference)
		{
			return ProcessSave(form, speaker => RedirectToAction<SpeakerController>(c => c.List(conference)),speaker => speaker.Conference=conference);
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(SpeakerForm form)
		{
			var result = new ValidationResult();
			if (SpeakerKeyAlreadyExists(form))
			{
				result.AddError<SpeakerForm>(x => x.Key, "This speaker key already exists");
			}

			return result.GetAllErrors();
		}

		private bool SpeakerKeyAlreadyExists(SpeakerForm message)
		{
			Speaker speaker = _repository.GetByKey(message.Key);
			return speaker != null && speaker.Id != message.Id;
		}

		[RequireAuthenticationFilter]
		public ActionResult New()
		{
			return View("Edit", new SpeakerForm());
		}

		[RequireAuthenticationFilter]
		public ActionResult Delete(Speaker speaker,Conference conference)
		{
            if(!_securityContext.HasPermissionsFor(speaker))
            {
                return NotAuthorizedView;
            }

		    if(_sessionsRepository.GetAllForSpeaker(speaker).Length==0)
		    {
			    _repository.Delete(speaker);
		    }
		    else
		    {
			    TempData.Add("message", "Speaker cannot be deleted.");
		    }
            return RedirectToAction<SpeakerController>(c => c.List(conference));
		}

	
	}
}