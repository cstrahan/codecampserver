using System;
using System.Web.Mvc;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Models
{
    public class ConferenceModelBinder : IModelBinder
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceModelBinder()
            :this(IoC.Resolve<IConferenceRepository>())
        {
        }

        public ConferenceModelBinder(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        public object GetValue(ControllerContext controllerContext, string modelName, Type modelType, ModelStateDictionary modelState)
        {
            if(modelType != typeof(Model.Domain.Conference))
                return null;

            var form = controllerContext.HttpContext.Request.Form;

            Guid id = new Guid(form["Id"]);
            var conference = id == Guid.Empty ? new Model.Domain.Conference() : _conferenceRepository.GetById(id);

            string name = form["Name"];
            if(string.IsNullOrEmpty(name))
                modelState.AddModelError("Name", name, "Name is required");

            conference.Name = form["Name"];
            conference.Key = form["Key"];            

            conference.StartDate = DateTime.Parse(form["StartDate"]);

            if(form["EndDate"] != string.Empty)
                conference.EndDate = DateTime.Parse(form["EndDate"]);          

            conference.Description = form["Description"];
            conference.PubliclyVisible = (form["PubliclyVisible"] != null);

            return conference;
        }
    }
}
