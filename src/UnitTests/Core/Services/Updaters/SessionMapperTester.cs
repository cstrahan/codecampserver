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
	public class SessionMapperTester : TestBase
	{
		[Test]
		public void Should_save_new_session_from_form()
		{
			var form = S<SessionForm>();
			var track = new Track();
			var timeSlot = new TimeSlot();
			var speaker = new Speaker();
			var conference = new Conference();
			form.Id = Guid.Empty;
			form.Track = track;
			form.TimeSlot = timeSlot;
			form.Speaker = speaker;
			form.Conference = conference;
			form.RoomNumber = "room";
			form.Title = "title";
			form.Abstract = "abstract";
			form.Level = SessionLevel.L200;
			form.MaterialsUrl = "url";
			form.Key = "key";

			var repository = M<ISessionRepository>();
			repository.Stub(s => s.GetById(form.Id)).Return(null);

			ISessionMapper mapper = new SessionMapper(repository);

			Session mapped = mapper.Map(form);

			mapped.Track.ShouldEqual(track);
			mapped.TimeSlot.ShouldEqual(timeSlot);
			mapped.Speaker.ShouldEqual(speaker);
			mapped.Conference.ShouldEqual(conference);
			mapped.RoomNumber.ShouldEqual("room");
			mapped.Title.ShouldEqual("title");
			mapped.Abstract.ShouldEqual("abstract");
			mapped.Level.ShouldEqual(SessionLevel.L200);
			mapped.MaterialsUrl.ShouldEqual("url");
			mapped.Key.ShouldEqual("key");
		}


		[Test]
		public void Should_map_existing_session_from_form()
		{
			var form = S<SessionForm>();
			var track = new Track();
			var timeSlot = new TimeSlot();
			var speaker = new Speaker();
			var conference = new Conference();
			form.Id = Guid.Empty;
			form.Track = track;
			form.TimeSlot = timeSlot;
			form.Speaker = speaker;
			form.Conference = conference;
			form.RoomNumber = "room";
			form.Title = "title";
			form.Abstract = "abstract";
			form.Level = SessionLevel.L200;
			form.MaterialsUrl = "url";
			form.Key = "key";

			var repository = M<ISessionRepository>();
			var session = new Session();
			repository.Stub(s => s.GetById(form.Id)).Return(session);

			ISessionMapper mapper = new SessionMapper(repository);

			Session mapped = mapper.Map(form);
			session.ShouldEqual(mapped);
			session.ShouldBeTheSameAs(session);
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
		}
	}
}