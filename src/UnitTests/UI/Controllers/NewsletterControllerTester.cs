using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class NewsletterControllerTester : SaveControllerTester
	{
		[Test]
		public void When_a_new_attendee_signs_up_for_the_newsletter_Index_action_should_render_the_default_view()
		{
			var repository = S<IConferenceRepository>();
			var conference = new Conference();
			repository.Stub(repo => repo.GetById(Guid.Empty)).Return(
				conference).IgnoreArguments();

			var controller = new NewsletterController(repository);

			ActionResult result = controller.Index();

			result.AssertViewRendered().ForView(ViewNames.Default);
		}

		[Test]
		public void When_a_new_attendee_signs_up_for_the_newsletter_Save_should_add_the_attendee_to_the_repository()
		{
			var repository = S<IConferenceRepository>();
			var conference = new Conference();
			repository.Stub(repo => repo.GetById(Guid.Empty)).Return(
				conference).IgnoreArguments();

			var controller = new NewsletterController(repository);

			ActionResult result = controller.Save(new AttendeeForm());

			result
				.AssertViewRendered()
				.ForView("index").TempData["message"].ShouldEqual(
				"You have subscribed to the newsletter");


			repository.AssertWasCalled(r => r.Save(null),
			                           o => o.IgnoreArguments());
		}
	}
}