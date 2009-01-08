using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class ConferenceUpdaterTester : TestBase
	{
		[Test]
		public void Should_update_an_existing_attendee()
		{
			var message = S<IAttendeeMessage>();
			message.AttendeeID = Guid.NewGuid();
			message.Status = AttendanceStatus.Confirmed;
			message.FirstName = "first";
			message.LastName = "last";
			message.Webpage = "http://myhomepage.com";

			
			var conference = new Conference();
			conference.AddAttendee(new Attendee {Id = message.AttendeeID.Value, Status = AttendanceStatus.NotAttending});

			var repository = M<IConferenceRepository>();
			repository.Stub(r => r.GetById(Guid.Empty)).IgnoreArguments().Return(conference);

			IConferneceUpdater updater = new ConferneceUpdater(repository);
			UpdateResult<Conference, IAttendeeMessage> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeTrue();

			Attendee attendee = result.Model.Attendees[0];
			
			attendee.Status.ShouldEqual(AttendanceStatus.Confirmed);
			attendee.FirstName.ShouldEqual( "first");
			attendee.LastName.ShouldEqual("last");
			attendee.Webpage.ShouldEqual("http://myhomepage.com");


			repository.AssertWasCalled(r=>r.Save(conference));
		}

		[Test]
		public void Should_check_for_the_conference_to_exist()
		{
			var foo = S<IAttendeeMessage>();
			foo.ConferenceID = Guid.NewGuid();

			var repository = M<IConferenceRepository>();
			repository.Stub(r => r.GetById(foo.ConferenceID)).Return(null);

			IConferneceUpdater updater = new ConferneceUpdater(repository);
			UpdateResult<Conference, IAttendeeMessage> result = updater.UpdateFromMessage(foo);
			result.Successful.ShouldBeFalse();
			result.GetMessages(m => m.ConferenceID)[0].ShouldEqual("Conference does not exist.");

			repository.AssertWasCalled(r => r.GetById(Guid.Empty), opt => opt.IgnoreArguments());
		}

		[Test]
		public void Should_check_for_unique_attendee_email_when_adding_new_attendee()
		{
			var foo = S<IAttendeeMessage>();
			foo.ConferenceID = Guid.NewGuid();
			foo.EmailAddress = "were@were.com";

			var repository = M<IConferenceRepository>();
			var conference = new Conference();
			conference.AddAttendee(new Attendee {EmailAddress = foo.EmailAddress});
			repository.Stub(r => r.GetById(foo.ConferenceID)).Return(conference);

			IConferneceUpdater updater = new ConferneceUpdater(repository);
			UpdateResult<Conference, IAttendeeMessage> result = updater.UpdateFromMessage(foo);
			result.Successful.ShouldBeFalse();
			result.GetMessages(m => m.EmailAddress)[0].ShouldEqual("Attended is already registered for this conference.");

			repository.AssertWasCalled(r => r.GetById(Guid.Empty), opt => opt.IgnoreArguments());
		}
	}
}