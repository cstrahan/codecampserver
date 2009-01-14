using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class TimeSlotControllerTester : SaveControllerTester
	{
		[Test]
		public void When_New_is_called_the_edit_view_should_be_rendered_with_the_time_slot()
		{
			var controller = new TimeSlotController(null, null);

			var conference = new Conference {Id = Guid.NewGuid(), Key = "foo"};

			ActionResult result = controller.New(conference);

			result.AssertViewRendered().ForView("edit");
			var form = ((TimeSlotForm)controller.ViewData.Model);
			form.ConferenceKey.ShouldEqual("foo");
			form.ConferenceId.ShouldEqual(conference.Id);
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
			var timeSlots = new []{new TimeSlot()};
			repository.Stub(repo => repo.GetAllForConference(conference)).Return(timeSlots);

			var mapper = S<ITimeSlotMapper>();
			mapper.Stub(m => m.Map(timeSlots)).Return(new[] {new TimeSlotForm()});
			var controller = new TimeSlotController(repository, mapper);

			ActionResult result = controller.Index(conference);

			result.AssertViewRendered().ForView(ViewNames.Default);
			var forms = ((TimeSlotForm[])controller.ViewData.Model);
			forms.Length.ShouldEqual(1);
		}

		[Test]
		public void Should_save_the_timeslot()
		{
			var form = new TimeSlotForm();
			var timeSlot = new TimeSlot();

			var mapper = S<ITimeSlotMapper>();
			mapper.Stub(m => m.Map(form)).Return(timeSlot);

			var repository = S<ITimeSlotRepository>();

			var controller = new TimeSlotController(repository, mapper);
			var conference = new Conference();
			var result = (RedirectToRouteResult) controller.Save(form, conference);

			repository.AssertWasCalled(r => r.Save(timeSlot));
			result.AssertActionRedirect().ToAction<TimeSlotController>(a => a.Index(conference));
		}

		[Test]
		public void Delete_should_delete_a_Session_and_render_index()
		{
			var conference = new Conference { Key = "foo" };
			var timeslot = new TimeSlot { Conference = conference };
			var repository = S<ITimeSlotRepository>();
			var controller = new TimeSlotController(repository, S<ITimeSlotMapper>());

			var result = controller.Delete(timeslot);

			repository.AssertWasCalled(x => x.Delete(timeslot));
			result
				.AssertActionRedirect()
				.ToAction<TimeSlotController>(x => x.Index(null));
				
		}

	}
}