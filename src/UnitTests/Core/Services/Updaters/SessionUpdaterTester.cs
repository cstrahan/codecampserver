using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.UnitTests;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class SessionUpdaterTester : TestBase
	{
		[Test]
		public void Should_save_new_session_from_message()
		{
			var message = S<SessionForm>();
			var track = new Track();
			var timeSlot = new TimeSlot();
			var speaker = new Speaker();
			var conference = new Conference();
			message.Id = Guid.Empty;
			message.Track = track;
			message.TimeSlot = timeSlot;
			message.Speaker = speaker;
			message.Conference = conference;
			message.RoomNumber = "room";
			message.Title = "title";
			message.Abstract = "abstract";
			message.Level = SessionLevel.L200;
			message.MaterialsUrl = "url";
			message.Key = "key";

			var repository = M<ISessionRepository>();
			repository.Stub(s => s.GetById(message.Id)).Return(null);

			ISessionUpdater updater = new SessionUpdater(repository);

			UpdateResult<Session, SessionForm> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeTrue();
			Session session = updateResult.Model;
			session.Track.ShouldEqual(track);
			session.TimeSlot.ShouldEqual(timeSlot);
			session.Speaker.ShouldEqual(speaker);
			session.Conference.ShouldEqual(conference);
			session.RoomNumber.ShouldEqual("room");
			session.Title.ShouldEqual("title");
			session.Abstract.ShouldEqual("abstract");
			session.Level.ShouldEqual(SessionLevel.L200);
			session.MaterialsUrl.ShouldEqual("url");
			session.Key.ShouldEqual("key");
			repository.AssertWasCalled(s => s.Save(session));
		}


		[Test]
		public void Should_update_existing_session_from_message()
		{
			var message = S<SessionForm>();
			var track = new Track();
			var timeSlot = new TimeSlot();
			var speaker = new Speaker();
			var conference = new Conference();
			message.Id = Guid.Empty;
			message.Track = track;
			message.TimeSlot = timeSlot;
			message.Speaker = speaker;
			message.Conference = conference;
			message.RoomNumber = "room";
			message.Title = "title";
			message.Abstract = "abstract";
			message.Level = SessionLevel.L200;
			message.MaterialsUrl = "url";
			message.Key = "key";

			var repository = M<ISessionRepository>();
			var existingSession = new Session();
			repository.Stub(s => s.GetById(message.Id)).Return(existingSession);

			ISessionUpdater updater = new SessionUpdater(repository);

			UpdateResult<Session, SessionForm> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeTrue();
			Session session = updateResult.Model;
			session.ShouldBeTheSameAs(existingSession);
			session.Track.ShouldEqual(track);
			session.TimeSlot.ShouldEqual(timeSlot);
			session.Speaker.ShouldEqual(speaker);
			session.Conference.ShouldEqual(conference);
			session.RoomNumber.ShouldEqual("room");
			session.Title.ShouldEqual("title");
			session.Abstract.ShouldEqual("abstract");
			session.Level.ShouldEqual(SessionLevel.L200);
			session.MaterialsUrl.ShouldEqual("url");
			session.Key.ShouldEqual("key");
			repository.AssertWasCalled(s => s.Save(session));
		}

		[Test]
		public void Should_not_add_new_session_if_key_already_exists()
		{
			var message = S<SessionForm>();
			message.Id = Guid.NewGuid();
			message.Key = "Key";

			var repository = M<ISessionRepository>();
			var session = new Session();
			repository.Stub(s => s.GetByKey("Key")).Return(session);

			ISessionUpdater updater = new SessionUpdater(repository);

			UpdateResult<Session, SessionForm> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeFalse();

			CollectionAssert.Contains(updateResult.GetMessages(x => x.Key), "This session key already exists");
		}
	}
}