using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class ConferenceRepositoryTester : KeyedRepositoryTester<Conference, ConferenceRepository>
	{
		private static Conference CreateConference()
		{
			var conference = new Conference
			                 	{
			                 		Name = "sdf",
			                 		Description = "description",
			                 		StartDate = new DateTime(2008, 12, 2),
			                 		EndDate = new DateTime(2008, 12, 3),
			                 		LocationName =
			                 			"St Edwards Professional Education Center",
			                 		Address = "12343 Research Blvd",
			                 		City = "Austin",
			                 		Region = "Tx",
			                 		PostalCode = "78234",
			                 		PhoneNumber = "512-555-1234"
			                 	};
			conference.AddAttendee(new Attendee {EmailAddress = "werwer@asdfasd.com"});
			return conference;
		}

		protected override ConferenceRepository CreateRepository()
		{
			return new ConferenceRepository(GetSessionBuilder());
		}

		[Test]
		public void Should_remove_an_attendee_from_its_collection()
		{
			Conference conference = CreateConference();

			IConferenceRepository repository = new ConferenceRepository(new HybridSessionBuilder());
			repository.Save(conference);
			conference.RemoveAttendee(conference.GetAttendees()[0]);
			repository.Save(conference);

			Conference rehydratedConference;
			using (ISession session = GetSession())
			{
				rehydratedConference = session.Load<Conference>(conference.Id);
				rehydratedConference.GetAttendees().Length.ShouldEqual(0);
			}
		}

		[Test]
		public void should_retrieve_conferences_for_a_usergroup()
		{
			var usergroup = new UserGroup();
			var conference1 = new Conference();
			var conference2 = new Conference();
			conference1.UserGroup = usergroup;

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(conference2);
				session.Flush();
			}

			IConferenceRepository repository = new ConferenceRepository(new HybridSessionBuilder());
			Conference[] conferences = repository.GetAllForUserGroup(usergroup);

			conferences.Length.ShouldEqual(1);
		}

		[Test]
		public void Should_retrieve_upcoming_conferences_for_a_usergroup()
		{
			SystemTime.Now = () => new DateTime(2009, 5, 5);
			var usergroup = new UserGroup();
			var conference1 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 2),
			                  		EndDate = new DateTime(2009, 4, 6)
			                  	};
			var conference4 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 3),
			                  		EndDate = new DateTime(2009, 5, 4, 20, 0, 0)
			                  	};
			var conference2 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 4),
			                  		EndDate = new DateTime(2009, 5, 5, 20, 0, 0)
			                  	};
			var conference3 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 5),
			                  		EndDate = new DateTime(2009, 5, 7)
			                  	};

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(conference2);
				session.SaveOrUpdate(conference3);
				session.SaveOrUpdate(conference4);
				session.Flush();
			}

			IConferenceRepository repository = new ConferenceRepository(new HybridSessionBuilder());
			Conference[] conferences = repository.GetFutureForUserGroup(usergroup);

			conferences.Length.ShouldEqual(2);
			conferences[0].ShouldEqual(conference2);
		}

		[Test]
		public void Should_update_an_attendee()
		{
			Conference conference = CreateConference();
			conference.GetAttendees()[0].Status = AttendanceStatus.Interested;

			IConferenceRepository repository = new ConferenceRepository(new HybridSessionBuilder());

			repository.Save(conference);
			conference.GetAttendees()[0].Status = AttendanceStatus.Confirmed;
			repository.Save(conference);

			Conference rehydratedConference = repository.GetById(conference.Id);
			rehydratedConference.GetAttendees()[0].Status.ShouldEqual(
				AttendanceStatus.Confirmed);
		}
	}
}