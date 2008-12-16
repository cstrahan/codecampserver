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
            var conferenceDtos =AutoMapper.Map(conferences, typeof (Conference[]), typeof (ConferenceForm[]), typeof (Conference),
                           typeof (ConferenceForm));
            return View(conferenceDtos);
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
                conference.Description = form.Description;
                conference.LocationName = form.LocationName;
                conference.City = form.City;
                conference.Address = form.Address;
                conference.City = form.City;
                conference.Region = form.Region;
                conference.PostalCode = form.PostalCode;
                conference.PhoneNumber = form.PhoneNumber;
                conference.StartDate = Convert.ToDateTime(form.StartDate);
                conference.EndDate = Convert.ToDateTime(form.EndDate);
                _repository.Save(conference);
                return RedirectToAction<ConferenceController>(c => c.Index());
            }
            return View("Edit");
        }
    }
}