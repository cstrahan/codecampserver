using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.Core.Services.Updaters.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class ConferenceControllerTester : TestControllerBase
	{
		[Test]
		public void
			When_a_conference_does_not_exist_Edit_should_redirect_to_the_index_with_a_message
			()
		{
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

			var controller = new ConferenceController(repository, null);

			ActionResult result = controller.Edit(Guid.Empty);
			result.AssertActionRedirect().ToAction<ConferenceController>(e => e.Index());
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

			ActionResult result = controller.Index();

			result.AssertActionRedirect().ToAction<ConferenceController>(a => a.New());
		}
		

		[Test]
		public void When_a_conference_exists_Save_should_update_the_confernce()
		{
			var form = new ConferenceForm();

			var updater = S<IConferenceUpdater>();
			updater.Stub(u => u.UpdateFromMessage(null)).IgnoreArguments().Return(ConferenceUpdater.Success());

			var controller = new ConferenceController(null,updater);
	
			controller.Save(form)
				.AssertActionRedirect()
				.ToAction<ConferenceController>(a => a.Index());

			updater.AssertWasCalled(u => u.UpdateFromMessage(form));
		}
	}
}