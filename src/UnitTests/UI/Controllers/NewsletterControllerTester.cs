using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.UI.UI.Controllers;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
    public class When_a_new_attendee_signs_up_for_the_newsletter: TestControllerBase<NewsletterController>
    {
        private IConferenceRepository conferenceRepository;

        protected override NewsletterController CreateController()
        {
            conferenceRepository = Mock<IConferenceRepository>();
            var conference = new Conference();            
            conferenceRepository.Stub(repo => repo.GetById(Guid.Empty)).Return(conference).IgnoreArguments();

            return new NewsletterController(conferenceRepository);
        }

        [Test]
        public void Index_action_should_render_the_default_view()
        {
            ActionResult result = controllerUnderTest.Index();

            result.AssertViewRendered().ForView(DEFAULT_VIEW);
        }

        [Test]
        public void Save_should_add_the_attendee_to_the_repository()
        {
            ActionResult result = controllerUnderTest.Save(new AttendeeForm());

            result
                .AssertViewRendered()
                .ForView("index").TempData["message"].ShouldEqual("You have subscribed to the newsletter");
                

            conferenceRepository.AssertWasCalled(r => r.Save(null), o => o.IgnoreArguments());
        }

    }

    //public class When_an_existing_attendee_signs_up_for_the_newsletter : TestControllerBase<NewsletterController>
    //{
    //    private IConferenceRepository potentialAttendeeRepository;

    //    protected override NewsletterController CreateController()
    //    {
    //        potentialAttendeeRepository = Mock<IConferenceRepository>();
    //        potentialAttendeeRepository.Stub(repo => repo.GetAll()).Return(new Attendee[0]);
    //        potentialAttendeeRepository.Stub(repo => repo.GetByEmail(null)).Return(new Attendee{}).IgnoreArguments();

    //        return new NewsletterController(potentialAttendeeRepository);
    //    }


    //    [Test]
    //    public void Save_should_message_that_the_attendee_is_already_on_the_list()
    //    {
    //        ActionResult result = controllerUnderTest.Save(new AttendeeForm());

    //        result
    //            .AssertViewRendered()
    //            .ForView("index").TempData["message"].ShouldNotBeNull();

    //    }
        
    //    [Test]
    //    public void Save_should_check_for_an_existing_attendee_by_email()
    //    {
    //        ActionResult result = controllerUnderTest.Save(new AttendeeForm());

    //        potentialAttendeeRepository.AssertWasCalled(r => r.GetByEmail(null), o => o.IgnoreArguments());
    //        result
    //            .AssertViewRendered()
    //            .ForView("index").TempData["message"].ShouldNotBeNull();
    //    }

    //}
    
}