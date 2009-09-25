using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class ConferenceControllerTester : SaveControllerTester
	{
		[Test]
		public void When_a_conference_does_not_exist_Edit_should_redirect_to_the_index_with_a_message()
		{
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

			var controller = new ConferenceController(repository, null,null);

			ActionResult result = controller.Edit((Conference)null);
			result.AssertActionRedirect().ToAction<ConferenceController>(e => e.List(null));
			controller.TempData["Message"].ShouldEqual(
				"Conference has been deleted.");
		}

		[Test]
		public void
			When_a_conference_does_not_exist_Index_action_should_redirect_to_new_when_conference_does_not_exist
			()
		{
		    var usergroup = new UserGroup();
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAllForUserGroup(usergroup)).Return(new Conference[0]);

			var controller = new ConferenceController(repository, null,null);

			ActionResult result = controller.List(usergroup);

			result.AssertActionRedirect().ToAction<ConferenceController>(a => a.New(null));
		}

		[Test]
		public void Should_save_the_conference()
		{
			var form = new ConferenceForm();
			var conference = new Conference();

			var mapper = S<IConferenceMapper>();
			mapper.Stub(m => m.Map(form)).Return(conference);

			var repository = S<IConferenceRepository>();
            var securityContext = S<ISecurityContext>();
            securityContext.Stub(context => context.HasPermissionsForUserGroup(Guid.Empty)).Return(true);

            var controller = new ConferenceController(repository, mapper, securityContext);
            var result = (RedirectToRouteResult)controller.Edit(form);

			repository.AssertWasCalled(r => r.Save(conference));
			result.AssertActionRedirect().ToAction<HomeController>(a => a.Index(null));
		}

	    [Test]
	    public void Should_prevent_a_user_that_is_not_and_admin_from_going_to_the_edit_screen()
	    {

            var securityContext = S<ISecurityContext>();
            securityContext.Stub(context => context.HasPermissionsFor(new Conference())).IgnoreArguments().Return(false);

            var controller = new ConferenceController(null, null, securityContext);
            var result = (ViewResult)controller.Edit(new Conference());

            result.AssertViewRendered().ViewName.ShouldEqual(ViewPages.NotAuthorized);
	    }
        [Test]
        public void Should_prevent_a_user_from_saving_a_conference_that_they_do_not_have_permissions_to_edit()
        {
            var securityContext = S<ISecurityContext>();
            securityContext.Stub(context => context.HasPermissionsFor(new Conference())).IgnoreArguments().Return(false);

            var controller = new ConferenceController(null, null, securityContext);
            var result = (ViewResult)controller.Edit(new ConferenceForm());

            result.AssertViewRendered().ViewName.ShouldEqual(ViewPages.NotAuthorized);
        }


		[Test]
		public void Should_not_save_conference_if_key_already_exists()
		{
			var form = new ConferenceForm {Key = "foo", Id = Guid.NewGuid()};
			var conference = new Conference();

			var mapper = S<IConferenceMapper>();
			mapper.Stub(m => m.Map(form)).Return(conference);

			var repository = S<IConferenceRepository>();
			repository.Stub(r => r.GetByKey("foo")).Return(new Conference());

		    var securityContext = S<ISecurityContext>();
		    securityContext.Stub(context => context.HasPermissionsForUserGroup(Guid.Empty)).Return(true);

		    var controller = new ConferenceController(repository, mapper, securityContext);
			var result = (ViewResult) controller.Edit(form);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Key"].Errors[0].ErrorMessage.ShouldEqual("This conference key already exists");
		}
	}
}