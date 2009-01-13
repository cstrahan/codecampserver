using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class AttendeeControllerTester : SaveControllerTester
	{
		[Test]
		public void Index_should_put_conference_attendees_in_viewdata()
		{
			var conference = new Conference();
			var attendee = new Attendee {EmailAddress = "test@example.com"};
			conference.AddAttendee(attendee);
			var attendee1 = new Attendee {EmailAddress = "foo@example.com"};
			conference.AddAttendee(attendee1);

			var controller = new AttendeeController(S<IAttendeeMapper>(), M<IConferenceRepository>());

			ViewResult result = controller.Index(conference);

			result.ViewData.Get<Attendee[]>().ShouldEqual(new[] {attendee, attendee1});
			result.ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void New_should_put_a_new_attendee_form_in_viewdata_with_controller_details_and_render_edit_view()
		{
			var conference = new Conference {Id = Guid.NewGuid(),};
			var controller = new AttendeeController(M<IAttendeeMapper>(), M<IConferenceRepository>());
			ViewResult result = controller.New(conference);
			result.ViewName.ShouldEqual("Edit");
			result.ViewData.Model.ShouldEqual(new AttendeeForm {ConferenceID = conference.Id});
		}

		[Test]
		public void Save_should_save_the_attendee_and_redirect_to_thankyou()
		{
			var conference = new Conference();
			conference.AddAttendee(new Attendee(){EmailAddress = "1"});
			var repository = S<IConferenceRepository>();
			repository.Stub(r => r.GetById(Guid.Empty)).IgnoreArguments().Return(conference);

			var mapper = S<IAttendeeMapper>();
			mapper.Stub(m => m.Map(new AttendeeForm())).IgnoreArguments().Return(conference);

			var controller = new AttendeeController(mapper, repository);
			
			var result = controller.Save(new AttendeeForm(){EmailAddress = "2"});

			result.AssertActionRedirect()
				.ToAction<AttendeeController>(c => c.Confirmation());
				

			repository.AssertWasCalled(r=>r.Save(conference));

		}
		[Test]
		public void Confirm_should_set_attendee_status_to_confirm()
		{
			var conference = new Conference();
			var attendee = new Attendee {Id = Guid.Empty, Status = AttendanceStatus.Attending};
			conference.AddAttendee(attendee);

			var mapper = M<IAttendeeMapper>();
			var repository = M<IConferenceRepository>();
			var controller = new AttendeeController(mapper, repository);

			controller.Confirm(conference, attendee.Id);
			attendee.Status.ShouldEqual(AttendanceStatus.Confirmed);
			repository.AssertWasCalled(x => x.Save(conference));
		}

		[Test]
		public void Confirm_should_404_if_no_attendee_exists()
		{
			var conference = new Conference();
			var attendee = new Attendee {Id = Guid.Empty, Status = AttendanceStatus.Attending};

			var controller = new AttendeeController(M<IAttendeeMapper>(), M<IConferenceRepository>());

			ViewResult confirm = controller.Confirm(conference, attendee.Id);
			confirm.ViewName.ShouldEqual(ViewNames.Response404);
			M<IConferenceRepository>().AssertWasNotCalled(x => x.Save(conference));
		}
	}
}