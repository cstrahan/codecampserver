using System;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class TimeSlotRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldGetTimeSlotsForConference()
        {
            var conference = new Conference("austincodecamp", "");

            using (ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.Flush();
            }

            var timeSlot1 = new TimeSlot(conference,
                                         new DateTime(2008, 1, 1, 8, 0, 0),
                                         new DateTime(2008, 1, 1, 9, 0, 0),
                                         "Morning Session 1");
            var timeSlot2 = new TimeSlot(conference,
                                         new DateTime(2008, 1, 1, 10, 0, 0),
                                         new DateTime(2008, 1, 1, 11, 0, 0),
                                         "Morning Session 2");

            using (ISession trackSession = getSession())
            {
                trackSession.SaveOrUpdate(timeSlot1);
                trackSession.SaveOrUpdate(timeSlot2);
                trackSession.Flush();
            }

            ITimeSlotRepository repository = new TimeSlotRepository(_sessionBuilder);
            TimeSlot[] timeSlots = repository.GetTimeSlotsFor(conference);

            Assert.That(timeSlots.Length, Is.EqualTo(2));
            Assert.That(timeSlots[0].StartTime, Is.EqualTo(new DateTime(2008, 1, 1, 8, 0, 0)));
            Assert.That(timeSlots[1].StartTime, Is.EqualTo(new DateTime(2008, 1, 1, 10, 0, 0)));
        }

        [Test]
        public void ShouldSaveTimeSlotToDatabase()
        {
            var conference = new Conference("austincodecamp", "")
                                 {
                                     StartDate = new DateTime(2008, 1, 1),
                                     EndDate = new DateTime(2008, 1, 1)
                                 };
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.Flush();
            }

            var timeSlot = new TimeSlot(conference,
                                        new DateTime(2008, 1, 1, 8, 0, 0),
                                        new DateTime(2008, 1, 1, 9, 0, 0),
                                        "Session");

            ITimeSlotRepository repository = new TimeSlotRepository(_sessionBuilder);
            repository.Save(timeSlot);

            TimeSlot rehydratedTimeSlot = null;
            //get Session back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedTimeSlot = session.Load<TimeSlot>(timeSlot.Id);

                Assert.That(rehydratedTimeSlot != null);
                Assert.That(rehydratedTimeSlot, Is.EqualTo(timeSlot));
                Assert.That(rehydratedTimeSlot.Conference, Is.EqualTo(conference));
                Assert.That(rehydratedTimeSlot.Purpose, Is.EqualTo("Session"));
                Assert.That(rehydratedTimeSlot.StartTime, Is.EqualTo(new DateTime(2008, 1, 1, 8, 0, 0)));
                Assert.That(rehydratedTimeSlot.EndTime, Is.EqualTo(new DateTime(2008, 1, 1, 9, 0, 0)));
            }
        }
    }
}