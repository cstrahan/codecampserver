using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
    public class When_a_conference_does_not_exist : TestControllerBase<ConferenceController>
    {
        private IConferenceRepository conferenceRepository;

        protected override ConferenceController CreateController()
        {
            conferenceRepository = Mock<IConferenceRepository>();
            conferenceRepository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

            return new ConferenceController(conferenceRepository);
        }

        [Test]
        public void Edit_should_redirect_to_the_index_with_a_message()
        {
            ActionResult result = controllerUnderTest.Edit(Guid.Empty);
            result.AssertActionRedirect().ToAction<ConferenceController>(e => e.Index());
            controllerUnderTest.TempData["Message"].ShouldEqual("Conference has been deleted.");

            
        }

        [Test]
        public void Index_action_should_redirect_to_new_when_conference_does_not_exist()
        {
            ActionResult result = controllerUnderTest.Index();

            result.AssertActionRedirect().ToAction<ConferenceController>(a => a.New());
        }
    }

    public class When_a_conference_exists : TestControllerBase<ConferenceController>
    {
        private IConferenceRepository _conferenceRepository;
        private Conference conference;

        protected override ConferenceController CreateController()
        {
            conference = new Conference {Name = "Austin Code Camp", Id = Guid.NewGuid()};
            _conferenceRepository = Mock<IConferenceRepository>();
            _conferenceRepository.Stub(repo => repo.GetAll()).Return(new[] {conference});

            return new ConferenceController(_conferenceRepository);
        }

        private ConferenceForm CreateConferenceForm()
        {
            return new ConferenceForm
                       {
                           Id = conference.Id,
                           Name = "Austin Code Camp",
                           Description = "This is a code camp!",
                           StartDate = "12/2/2008",
                           EndDate = "12/3/2008",
                           LocationName = "St Edwards Professional Education Center",
                           Address = "1234 Main St",
                           City = "Austin",
                           Region = "Texas",
                           PostalCode = "78787",
                           PhoneNumber = "512-555-1234",
                           Key = "AustinCodeCamp2008"
                       };
        }

        [Test]
        public void Save_should_a_valid_user()
        {
            ConferenceForm form = CreateConferenceForm();

            _conferenceRepository.Stub(c => c.GetById(conference.Id)).Return(conference);

            ActionResult result = controllerUnderTest.Save(form);

            result
                .AssertActionRedirect()
                .ToAction<ConferenceController>(a => a.Index());

            conference.Name.ShouldEqual("Austin Code Camp");
            conference.Description.ShouldEqual("This is a code camp!");
            conference.StartDate.ShouldEqual(DateTime.Parse("12/2/2008"));
            conference.EndDate.ShouldEqual(DateTime.Parse("12/3/2008"));
            conference.LocationName.ShouldEqual("St Edwards Professional Education Center");
            conference.Address.ShouldEqual("1234 Main St");
            conference.City.ShouldEqual("Austin");
            conference.Region.ShouldEqual("Texas");
            conference.PostalCode.ShouldEqual("78787");
            conference.PhoneNumber.ShouldEqual("512-555-1234");
            conference.Key.ShouldEqual("AustinCodeCamp2008");
            _conferenceRepository.AssertWasCalled(r => r.Save(conference));
        }
    }
}