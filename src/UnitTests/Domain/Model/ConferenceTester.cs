using System;
using CodeCampServer.Domain.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Domain.Model
{
    [TestFixture]
    public class ConferenceTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Conference();
        }

        [Test]
        public void ShouldBeAbleToAddManyTimeslots()
        {
            Conference conference = new Conference();
            conference.AddTimeSlot(new DateTime(), new DateTime());
            conference.AddTimeSlot(new DateTime(), new DateTime());

            TimeSlot[] timeslots = conference.GetTimeSlots();
            Assert.That(timeslots.Length, Is.EqualTo(2));
        }

        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "Time slot must be within conference.")]
        public void ShouldDisallowAddingATimeslotThatIsBeforeConference()
        {
            Conference conference = new Conference();
            conference.StartDate = new DateTime(2000, 1, 1);
            conference.EndDate = new DateTime(2000, 1, 2);
            conference.AddTimeSlot(new DateTime(1999, 12, 31, 11, 59, 59), new DateTime(2000, 1, 1));
        }

        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "Time slot must be within conference.")]
        public void ShouldDisallowAddingATimeslotThatIsAfterConference()
        {
            Conference conference = new Conference();
            conference.StartDate = new DateTime(2000, 1, 1);
            conference.EndDate = new DateTime(2000, 1, 2);
            conference.AddTimeSlot(new DateTime(2000, 1, 1), new DateTime(2000, 1, 3));
        }

        [Test]
        public void ShouldAcceptSponsors()
        {
            Conference conference = new Conference();
            Sponsor sponsor = new Sponsor();
            Sponsor sponsor2 = new Sponsor();
            conference.AddSponsor(sponsor);
            conference.AddSponsor(sponsor2);

            Sponsor[] sponsors = conference.GetSponsors();
            Assert.That(sponsors.Length, Is.EqualTo(2));
        }
    }
}