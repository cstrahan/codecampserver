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
            slot.AddSession(new Session(new Speaker(), "Session 1", "Session 1 abstract"));
            slot.AddSession(new Session(new Speaker(), "Session 2", "Session 2 abstract"));

            Session[] sessions = slot.GetSessions();
            Assert.That(sessions.Length, Is.EqualTo(2));
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
