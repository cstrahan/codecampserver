using System;
using System.Collections.Generic;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.DataAccess
{
	[TestFixture]
	public class AttendeeRepositoryTester : DatabaseTesterBase
	{
		[Test]
		public void ShouldGetAttendeesMatchingConference()
		{
			Conference theConference = new Conference("foo", "");
			Attendee attendee1a = new Attendee("jima", "foo", "http://www.www.coma", "some commenta", theConference, "a@b.com", "password", "salt");
			Attendee attendee1b = new Attendee("jimb", "foo", "http://www.www.comb", "some commentb", theConference, "a@b.com", "password", "salt");
			Conference anotherConference = new Conference("bar", "");
			Attendee attendee2 = new Attendee("pam", "foo", "http://www.yahoo.com", "comment", anotherConference, "a@b.com", "password", "salt");

			using (ISession session = getSession())
			{
				session.SaveOrUpdate(theConference);
				session.SaveOrUpdate(anotherConference);
				session.SaveOrUpdate(attendee1a);
				session.SaveOrUpdate(attendee1b);
				session.SaveOrUpdate(attendee2);
				session.Flush();
			}

			IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
			IEnumerable<Attendee> matchingAttendees = repository.GetAttendeesForConference(theConference);
			List<Attendee> attendees = new List<Attendee>(matchingAttendees);
			attendees.Sort(delegate(Attendee x, Attendee y) { return x.Contact.FirstName.CompareTo(y.Contact.FirstName); });

			Assert.That(attendees.Count, Is.EqualTo(2));
			Assert.That(attendees[0].Conference, Is.EqualTo(theConference));
			Assert.That(attendees[0].Contact.FirstName, Is.EqualTo("jima"));
			Assert.That(attendees[0].Website, Is.EqualTo("http://www.www.coma"));
			Assert.That(attendees[0].Comment, Is.EqualTo("some commenta"));

			Assert.That(attendees[1].Conference, Is.EqualTo(theConference));
			Assert.That(attendees[1].Contact.FirstName, Is.EqualTo("jimb"));
			Assert.That(attendees[1].Website, Is.EqualTo("http://www.www.comb"));
			Assert.That(attendees[1].Comment, Is.EqualTo("some commentb"));
		}

		[Test]
		public void ShouldSaveAttendeeToDatabase()
		{
			Conference anConference = new Conference("party", "");
			using (ISession session = getSession())
			{
				session.SaveOrUpdate(anConference);
				session.Flush();
			}

			Attendee attendee =
				new Attendee("Jeffrey", "Palermo", "http://www.jeffreypalermo.com", "the comment", anConference,
							 "me@jeffreypalermo.com", "password", "salt");
			IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
			repository.Save(attendee);

			Attendee rehydratedAttendee = null;
			//get Attendee back from database to ensure it was saved correctly
			using (ISession session = getSession())
			{
				rehydratedAttendee = session.Load<Attendee>(attendee.Id);

				Assert.That(rehydratedAttendee != null);
				Assert.That(rehydratedAttendee.Contact.FirstName, Is.EqualTo("Jeffrey"));
				Assert.That(rehydratedAttendee.Website, Is.EqualTo("http://www.jeffreypalermo.com"));
				Assert.That(rehydratedAttendee.Comment, Is.EqualTo("the comment"));
				Assert.That(rehydratedAttendee.Conference, Is.EqualTo(anConference));
			}
		}

		[Test]
		public void ShouldPullBackFirstPageOfAttendees()
		{
			Conference codeCamp2008 = new Conference("austincodecamp2008", "Austin Code Camp 2008");
			load100Attendees(codeCamp2008);

			resetSession();

			IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
			IEnumerable<Attendee> attendees = repository.GetAttendeesForConference(codeCamp2008, 1, 10);
			List<Attendee> attendeeList = new List<Attendee>(attendees);

			Assert.That(attendeeList.Count, Is.EqualTo(10));
			Assert.That(attendeeList[0].Comment, Is.EqualTo("000"));
			Assert.That(attendeeList[9].Comment, Is.EqualTo("009"));
		}

		[Test]
		public void ShouldPullBackProperPageOfAttendees()
		{
			Conference codeCamp2008 = new Conference("austincodecamp2008", "Austin Code Camp 2008");
			load100Attendees(codeCamp2008);

			resetSession();

			IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
			IEnumerable<Attendee> attendees = repository.GetAttendeesForConference(codeCamp2008, 3, 20);
			List<Attendee> attendeeList = new List<Attendee>(attendees);

			Assert.That(attendeeList.Count, Is.EqualTo(20));
			Assert.That(attendeeList[0].Comment, Is.EqualTo("040"));
			Assert.That(attendeeList[19].Comment, Is.EqualTo("059"));
		}

		[Test]
		public void ShouldRetrieveAttendeeByEmail()
		{
			Conference anConference = new Conference("tea party", "");
			using (ISession session = getSession())
			{
				session.SaveOrUpdate(anConference);
				session.Flush();
			}
			string email = "brownie@brownie.com.au";
			Attendee attendee =
				new Attendee("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", anConference,
							 email, "password", "salt");
			IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
			repository.Save(attendee);

			Attendee rehydratedAttendee = null;
			//get Attendee back from database to ensure it was saved correctly
			using (ISession session = getSession())
			{
				rehydratedAttendee = repository.GetAttendeeByEmail(email);

				Assert.That(rehydratedAttendee != null);
				Assert.That(rehydratedAttendee.Contact.FirstName, Is.EqualTo("Andrew"));
				Assert.That(rehydratedAttendee.Website, Is.EqualTo("http://blog.brownie.com.au"));
				Assert.That(rehydratedAttendee.Comment, Is.EqualTo("the comment"));
				Assert.That(rehydratedAttendee.Conference, Is.EqualTo(anConference));
			}
		}

		[Test]
		public void VerifyReturnsNullIfAttendeeDoesNotExist()
		{
			IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
			Attendee rehydratedAttendee = null;
			//get Attendee back from database to ensure it was saved correctly
			using (ISession session = getSession())
			{
				rehydratedAttendee = repository.GetAttendeeByEmail("nonexistingacccount@brownie.com.au");

				Assert.That(rehydratedAttendee == null);
			}
		}

		private void load100Attendees(Conference codeCamp2008)
		{
			using (ISession session = getSession())
			{
				codeCamp2008.StartDate = new DateTime(2008, 11, 26, 19, 30, 00);

				session.SaveOrUpdate(codeCamp2008);

				for (int i = 0; i < 100; i++)
				{
					Attendee attendee = createAttendee(codeCamp2008, i.ToString().PadLeft(3, '0'));
					session.SaveOrUpdate(attendee);
				}
				session.Flush();
			}
		}

		private Attendee createAttendee(Conference conference, string suffix)
		{
			Attendee attendee = new Attendee(suffix, suffix, suffix,
											 suffix, conference, suffix, suffix, suffix);
			return attendee;
		}
	}
}
