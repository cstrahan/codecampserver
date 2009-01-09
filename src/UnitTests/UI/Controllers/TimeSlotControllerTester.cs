using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.Core.Services.Updaters.Impl;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class TimeSlotControllerTester : TestControllerBase
	{
		[Test]
		public void When_New_is_called_the_edit_view_should_be_rendered_with_the_conference()
		{
			var controller = new TimeSlotController(null, null);

			var conference = new Conference {Id = Guid.NewGuid()};

			ActionResult result = controller.New(conference);

			result
				.AssertViewRendered()
				.ForView("edit")
				.ViewData.Get<TimeSlot>().Conference.Id.ShouldEqual(conference.Id);
		}

		[Test]
		public void When_a_timeslot_does_not_exist_Edit_should_redirect_to_the_index_with_a_message()
		{
			var controller = new TimeSlotController(null, null);

			ActionResult result = controller.Edit(null);
			result.AssertActionRedirect().ToAction<TimeSlotController>(e => e.Index(new Conference()));
			controller.TempData["Message"].ShouldEqual("Time slot has been deleted.");
		}

		[Test]
		public void When_a_timeslot_exists_Index_action_should_bind_the_tracks_for_a_conference()
		{
			var conference = new Conference();

			var repository = S<ITimeSlotRepository>();
			repository.Stub(repo => repo.GetAllForConference(conference)).Return(new TimeSlot[1]);

			var controller = new TimeSlotController(repository, null);

			ActionResult result = controller.Index(conference);

			result
				.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ViewData.Get<TimeSlot[]>().Length.ShouldEqual(1);
		}

		[Test]
		public void When_a_timeslot_exists_Save_should_update_the_timeslot()
		{
			var form = new TimeSlotForm();

			var updater = S<ITimeSlotUpdater>();
			updater.Stub(u => u.UpdateFromMessage(null)).IgnoreArguments().Return(
				ModelUpdater<TimeSlot, ITimeSlotMessage>.Success());

			var controller = new TimeSlotController(null, updater);
			var conference = new Conference();
			controller.Save(form, conference)
				.AssertActionRedirect()
				.ToAction<TimeSlotController>(a => a.Index(conference));

			updater.AssertWasCalled(u => u.UpdateFromMessage(form));
		}
	}
}