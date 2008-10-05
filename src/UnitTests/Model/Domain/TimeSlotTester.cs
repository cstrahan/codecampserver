using System;
using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class TimeSlotTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new TimeSlot();
        }

        [Test]
        public void ShouldBeAbleToAddManySessions()
        {
            Conference conference = new Conference();
            TimeSlot slot = new TimeSlot() { Conference = conference };

            Track track = new Track(conference, "Track Name");

            slot.AddSession(new Session(conference, new Person(), "Session 1", "Session 1 abstract"));
            slot.AddSession(new Session(conference, new Person(), "Session 2", "Session 2 abstract", track));

            Session[] sessions = slot.GetSessions();
            Assert.That(sessions.Length, Is.EqualTo(2));
        }

        [Test, ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Cannot have two sessions in the same track at the same time.")]
        public void ShouldOnlyBeAbleToAddOneSessionFromEachTrack()
        {
            Conference conference = new Conference();
            TimeSlot slot = new TimeSlot() { Conference = conference };
            Track track = new Track(conference, "Track Name");

            slot.AddSession(new Session(conference, new Person(), "Session 1", "Session 1 abstract", track));
            slot.AddSession(new Session(conference, new Person(), "Session 2", "Session 2 abstract", track));
        }

        [Test]
        public void ShouldBeAbleToRemoveSession()
        {
            Conference conference = new Conference();
            TimeSlot slot = new TimeSlot() { Conference = conference };

            Session session1 = new Session(conference, new Person(), "Session 1", "Session 1 abstract");
            slot.AddSession(session1);

            Track track = new Track(conference, "Track Name");
            var session2 = new Session(conference, new Person(), "Session 2", "Session 2 abstract", track);
            slot.AddSession(session2);

            slot.RemoveSession(session1);

            Session[] sessions = slot.GetSessions();
            Assert.That(sessions.Length, Is.EqualTo(1));
            Assert.That(sessions[0] == session2);
            
        }

        //[Test, ExpectedException(typeof(Exception), ExpectedMessage = "Time slot must be within conference.")]
        //public void ShouldDisallowAddingATimeslotThatIsBeforeConference()
        //{
        //    Conference conference = new Conference();
        //    conference.StartDate = new DateTime(2000, 1, 1);
        //    conference.EndDate = new DateTime(2000, 1, 2);
        //    conference.AddTimeSlot(new DateTime(1999, 12, 31, 11, 59, 59), new DateTime(2000, 1, 1), "");
        //}

        //[Test, ExpectedException(typeof(Exception), ExpectedMessage = "Time slot must be within conference.")]
        //public void ShouldDisallowAddingATimeslotThatIsAfterConference()
        //{
        //    Conference conference = new Conference();
        //    conference.StartDate = new DateTime(2000, 1, 1);
        //    conference.EndDate = new DateTime(2000, 1, 2);
        //    conference.AddTimeSlot(new DateTime(2000, 1, 1), new DateTime(2000, 1, 3), "");
        //}
    }
}
