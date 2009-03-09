using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class UserGroupControllerTester : SaveControllerTester
	{
		[Test]
		public void When_a_conference_does_not_exist_Edit_should_redirect_to_the_index_with_a_message()
		{
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

			var controller = new ConferenceController(repository, null);

			ActionResult result = controller.Edit(null);
			result.AssertActionRedirect().ToAction<ConferenceController>(e => e.List());
			controller.TempData["Message"].ShouldEqual(
				"Conference has been deleted.");
		}

		[Test]
		public void
			When_a_conference_does_not_exist_Index_action_should_redirect_to_new_when_conference_does_not_exist
			()
		{
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

			var controller = new ConferenceController(repository, null);

			ActionResult result = controller.List();

			result.AssertActionRedirect().ToAction<ConferenceController>(a => a.New());
		}

		[Test]
		public void Should_save_the_conference()
		{
			var form = new ConferenceForm();
			var conference = new Conference();

			var mapper = S<IConferenceMapper>();
			mapper.Stub(m => m.Map(form)).Return(conference);

			var repository = S<IConferenceRepository>();

			var controller = new ConferenceController(repository, mapper);
			var result = (RedirectToRouteResult) controller.Save(form);

			repository.AssertWasCalled(r => r.Save(conference));
			result.AssertActionRedirect().ToAction<ConferenceController>(a => a.List());
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

			var controller = new ConferenceController(repository, mapper);
			var result = (ViewResult) controller.Save(form);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Key"].Errors[0].ErrorMessage.ShouldEqual("This conference key already exists");
		}
	}
}