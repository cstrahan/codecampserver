using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
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
	}
}