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
			IConferenceRepository repository = mocks.CreateMock<IConferenceRepository>();
			Conference expectedConference = new Conference();
			SetupResult.For(repository.GetConferenceByKey("foo")).Return(expectedConference);
			mocks.ReplayAll();

			IConferenceService service = new ConferenceService(repository, null);
			Conference actualConference = service.GetConference("foo");

			Assert.That(actualConference, Is.EqualTo(expectedConference));
		}

		[Test]
		public void RegisteringAttendeeShouldSaveToRepository()
		{
			MockRepository mocks = new MockRepository();
			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			Attendee expectedAttendee = new Attendee();
			repository.SaveAttendee(expectedAttendee);
			mocks.ReplayAll();

			IConferenceService service = new ConferenceService(null, repository);
			service.RegisterAttendee(expectedAttendee);

			mocks.VerifyAll();
		}

		[Test]
		public void GetAttendeesShouldUseRepositoryAndRespectPageInfo()
		{
			MockRepository mocks = new MockRepository();
			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			Conference targetConference = new Conference();
			Attendee[] toReturn = new Attendee[] {new Attendee(), new Attendee()};
			SetupResult.For(repository.GetAttendeesForConference(targetConference, 2, 3)).Return(
				toReturn);
			mocks.ReplayAll();

			IConferenceService service = new ConferenceService(null, repository);
			IEnumerable<Attendee> attendees = service.GetAttendees(targetConference, 2, 3);
			List<Attendee> attendeesList = new List<Attendee>(attendees);
			
			Assert.That(attendeesList.ToArray(), Is.EqualTo(toReturn));
		}
	}
}