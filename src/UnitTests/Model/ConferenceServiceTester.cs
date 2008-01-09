using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Model
{
	[TestFixture]
	public class ConferenceServiceTester
	{
		[Test]
		public void ShouldGetConferenceByKey()
		{
			MockRepository mocks = new MockRepository();
			ILoginService loginService = mocks.CreateMock<ILoginService>();
			IConferenceRepository repository = mocks.CreateMock<IConferenceRepository>();
			Conference expectedConference = new Conference();
			SetupResult.For(repository.GetConferenceByKey("foo")).Return(expectedConference);
			mocks.ReplayAll();

			IConferenceService service = new ConferenceService(repository, null, loginService);
			Conference actualConference = service.GetConference("foo");

			Assert.That(actualConference, Is.EqualTo(expectedConference));
		}

		[Test]
		public void RegisteringAttendeeShouldSaveToRepository()
		{
			MockRepository mocks = new MockRepository();
			ILoginService loginService = mocks.CreateMock<ILoginService>();
			Expect.Call(loginService.CreateSalt()).Return("salt");
			Expect.Call(loginService.CreatePasswordHash("password", "salt")).Return("gobblygook");

			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			Attendee actualAttendee = null;
			repository.Save(null);
			LastCall.IgnoreArguments().Do(new Action<Attendee>(delegate(Attendee obj) { actualAttendee = obj; }));
			mocks.ReplayAll();

			IConferenceService service = new ConferenceService(null, repository, loginService);
			Conference conference = new Conference();
			Attendee attendee = service.RegisterAttendee("fn", "ln", "w", "c", conference, "email", "password");

			mocks.VerifyAll();

			Assert.That(actualAttendee, Is.EqualTo(attendee));
			Assert.That(attendee.Password, Is.EqualTo("gobblygook"));
			Assert.That(attendee.PasswordSalt, Is.EqualTo("salt"));
			Assert.That(attendee.GetName(), Is.EqualTo("fn ln"));
			Assert.That(attendee.Website, Is.EqualTo("w"));
			Assert.That(attendee.Comment, Is.EqualTo("c"));
			Assert.That(attendee.Contact.Email, Is.EqualTo("email"));
			Assert.That(attendee.Conference, Is.EqualTo(conference));
		}

		[Test]
		public void GetAttendeesShouldUseRepositoryAndRespectPageInfo()
		{
			MockRepository mocks = new MockRepository();
			ILoginService loginService = mocks.CreateMock<ILoginService>();
			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			Conference targetConference = new Conference();
			Attendee[] toReturn = new Attendee[] {new Attendee(), new Attendee()};
			SetupResult.For(repository.GetAttendeesForConference(targetConference, 2, 3)).Return(
				toReturn);
			mocks.ReplayAll();

			IConferenceService service = new ConferenceService(null, repository, loginService);
			IEnumerable<Attendee> attendees = service.GetAttendees(targetConference, 2, 3);
			List<Attendee> attendeesList = new List<Attendee>(attendees);

			Assert.That(attendeesList.ToArray(), Is.EqualTo(toReturn));
		}
	}
}