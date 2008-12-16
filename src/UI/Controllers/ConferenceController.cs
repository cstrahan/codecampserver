using System;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Filters;
using CodeCampServer.UI.Models.AutoMap;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
    public class ConferenceController : SmartController
    {
        private readonly IConferenceRepository _repository;

        public ConferenceController(IConferenceRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            Conference[] conferences = _repository.GetAll();
            if (conferences.Length < 1)
            {
                return RedirectToAction<ConferenceController>(c => c.Edit());
            }
            return View();
        }

        public ActionResult Edit()
        {
            Conference conference = _repository.GetAll().FirstOrDefault();

            if (conference == null)
            {
                conference = new Conference {StartDate = SystemTime.Now(), EndDate = SystemTime.Now()};
                _repository.Save(conference);
            }

            object form = AutoMapper.Map(conference, typeof (Conference), typeof (ConferenceForm));

            return View(form);
        }

        [ValidateModel(typeof(ConferenceForm))]
        public ActionResult Save([Bind(Prefix = "")] ConferenceForm form)
        {
            if(ModelState.IsValid)
            {
                var conference = _repository.GetById(form.Id);
                conference.Name = form.Name;
                _repository.Save(conference);
                return RedirectToAction<ConferenceController>(c => c.Index());
            }
            return View("Edit");
        }
    }
}