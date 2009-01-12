using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class AttendeeMapperTester : TestBase
	{
		[Test]
		public void Should_map_a_new_attendee()
		{
			var form = S<AttendeeForm>();
			form.AttendeeID = Guid.NewGuid();
			form.Status = AttendanceStatus.Confirmed;
			form.FirstName = "first";
			form.LastName = "last";
			form.Webpage = "http://myhomepage.com";

			var conference = new Conference();
			var repository = M<IConferenceRepository>();
			repository.Stub(r => r.GetById(Guid.Empty)).IgnoreArguments().Return(conference);

			IAttendeeMapper mapper = new AttendeeMapper(repository);
			Conference mapped = mapper.Map(form);
			mapped.GetAttendees().Length.ShouldEqual(1);
			Attendee attendee = mapped.GetAttendees()[0];
			attendee.Status.ShouldEqual(AttendanceStatus.Confirmed);
			attendee.FirstName.ShouldEqual("first");
			attendee.LastName.ShouldEqual("last");
			attendee.Webpage.ShouldEqual("http://myhomepage.com");
		}

		[Test]
		public void Should_map_an_existing_attendee()
		{
			var form = S<AttendeeForm>();
			form.AttendeeID = Guid.NewGuid();
			form.Status = AttendanceStatus.Confirmed;
			form.FirstName = "first";
			form.LastName = "last";
			form.Webpage = "http://myhomepage.com";

			var conference = new Conference();
			conference.AddAttendee(new Attendee {Id = form.AttendeeID.Value, Status = AttendanceStatus.Interested});

			var repository = M<IConferenceRepository>();
			repository.Stub(r => r.GetById(Guid.Empty)).IgnoreArguments().Return(conference);

			IAttendeeMapper mapper = new AttendeeMapper(repository);
			Conference mapped = mapper.Map(form);
			mapped.GetAttendees().Length.ShouldEqual(1);
			Attendee attendee = mapped.GetAttendees()[0];

			attendee.Status.ShouldEqual(AttendanceStatus.Confirmed);
			attendee.FirstName.ShouldEqual("first");
			attendee.LastName.ShouldEqual("last");
			attendee.Webpage.ShouldEqual("http://myhomepage.com");
		}
	}
}