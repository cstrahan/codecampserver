using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SessionRepositoryTester : KeyedRepositoryTester<Session, SessionRepository>
	{
		protected override SessionRepository CreateRepository()
		{
			return new SessionRepository(GetSessionBuilder());
		}

		[Test]
		public void Should_get_all_tracks_for_conference()
		{
			var conference = new Conference();
			var conference2 = new Conference();

			var session = new Session { Conference = conference };
			var session1 = new Session { Conference = conference };
			var session2 = new Session { Conference = conference2 };

			PersistEntities(conference, conference2, session, session1, session2);

			ISessionRepository repository = CreateRepository();
			Session[] sessions = repository.GetAllForConference(conference);
			CollectionAssert.Contains(sessions, session);
			CollectionAssert.Contains(sessions, session1);
			CollectionAssert.DoesNotContain(sessions, session2);
		}

		[Test]
		public void Should_find_sessions_by_timeslot()
		{
			var conference = new Conference();

			var timeSlot = new TimeSlot() { Conference = conference, StartTime = new DateTime(2000, 2, 1) };
			var session = new Session { Conference = conference, TimeSlot = timeSlot };

			var timeSlot1 = new TimeSlot() { Conference = conference, StartTime = new DateTime(2000, 2, 1) };
			var session1 = new Session { Conference = conference, TimeSlot = timeSlot1 };

			PersistEntities(conference, timeSlot, timeSlot1, session, session1);

			var repository = CreateRepository();
			
			Session[] sessions = repository.GetAllForTimeSlot(timeSlot);
			
			sessions.Length.ShouldEqual(1);
			sessions[0].ShouldEqual(session);
		}


		[Test]
		public void Should_find_sessions_by_track()
		{
			var conference = new Conference();

			var track = new Track() { Conference = conference};
			var session = new Session { Conference = conference, Track = track };

			var track1 = new Track() { Conference = conference};
			var session1 = new Session { Conference = conference, Track = track1 };

			PersistEntities(conference, track, track1, session, session1);

			ISessionRepository repository = CreateRepository();

			Session[] sessions = repository.GetAllForTrack(track);

			sessions.Length.ShouldEqual(1);
			sessions[0].ShouldEqual(session);
		}
	}
}