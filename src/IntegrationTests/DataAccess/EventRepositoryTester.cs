using System;
using System.Collections.Generic;
using CodeCampServer.DataAccess;
using CodeCampServer.Domain;
using CodeCampServer.Domain.Model;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class EventRepositoryTester : RepositoryBase
    {
        [Test]
        public void ShouldGetAllEvents()
        {
            TestHelper.EmptyDatabase();
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(new Event("thekey", "theName"));
                Event theEvent = new Event("thekey2", "theName2");
                theEvent.StartDate = new DateTime(2007, 1, 1, 11, 59, 30);
                theEvent.EndDate = new DateTime(2007, 1, 1, 11, 59, 31);
                theEvent.SponsorInfoHtml = string.Join("a", new string[100001]);
                theEvent.Location.Name = "locationname";
                theEvent.Location.Address1 = "locationaddress1";
                theEvent.Location.Address2 = "locationaddress2";
                theEvent.Location.PhoneNumber = "512-555-5555";
                session.SaveOrUpdate(theEvent);

                session.Flush();
            }

            IEventRepository repository = new EventRepository();
            IEnumerable<Event> events = repository.GetAllEvents();
            List<Event> eventList = new List<Event>(events);
            eventList.Sort(delegate(Event x, Event y) { return x.Key.CompareTo(y.Key); });

            Assert.That(eventList.Count, Is.EqualTo(2));
            Assert.That(eventList[0].Key, Is.EqualTo("thekey"));
            Assert.That(eventList[0].Name, Is.EqualTo("theName"));
            Assert.That(eventList[0].StartDate, Is.Null);
            Assert.That(eventList[0].EndDate, Is.Null);
            Assert.That(eventList[0].SponsorInfoHtml, Is.Null);
            Assert.That(eventList[0].Location, Is.Null);

            Assert.That(eventList[1].Key, Is.EqualTo("thekey2"));
            Assert.That(eventList[1].Name, Is.EqualTo("theName2"));
            Assert.That(eventList[1].StartDate, Is.EqualTo(DateTime.Parse("1/1/2007, 11:59:30 am")));
            Assert.That(eventList[1].EndDate, Is.EqualTo(DateTime.Parse("1/1/2007, 11:59:31 am")));
            Assert.That(eventList[1].SponsorInfoHtml.Length, Is.EqualTo(100000));
            Assert.That(eventList[1].Location.Name, Is.EqualTo("locationname"));
            Assert.That(eventList[1].Location.Address1, Is.EqualTo("locationaddress1"));
            Assert.That(eventList[1].Location.Address2, Is.EqualTo("locationaddress2"));
            Assert.That(eventList[1].Location.PhoneNumber, Is.EqualTo("512-555-5555"));

        }

        [Test]
        public void GetByKey()
        {
            TestHelper.EmptyDatabase();
            Event theEvent = new Event("Frank", "some name");
            Event theEvent2 = new Event("Frank2", "some name2");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theEvent);
                session.SaveOrUpdate(theEvent2);
                session.Flush();
            }

            IEventRepository repository = new EventRepository();
            Event eventSaved = repository.GetEventByKey("Frank");

            Assert.That(eventSaved, Is.Not.Null);
            Assert.That(eventSaved, Is.EqualTo(theEvent));
            Assert.That(theEvent.Key, Is.EqualTo("Frank"));
            Assert.That(theEvent.Name, Is.EqualTo("some name"));
        }

        [Test]
        public void ShouldGetNextEventBasedOnDate()
        {
            TestHelper.EmptyDatabase();
            Event oldEvent = new Event("2006", "past event");
            oldEvent.StartDate = new DateTime(2006, 1, 1);
            Event nextEvent = new Event("2007", "next event");
            nextEvent.StartDate = new DateTime(2007, 5, 5);
            Event futureEvent = new Event("2008", "future event");
            futureEvent.StartDate = new DateTime(2008, 1, 1);

            using(ISession session = getSession())
            {
                session.SaveOrUpdate(oldEvent);
                session.SaveOrUpdate(nextEvent);
                session.SaveOrUpdate(futureEvent);
                session.Flush();
            }

            IEventRepository repository = new EventRepository();
            Event matchingEvent = repository.GetFirstEventAfterDate(new DateTime(2006, 1, 2));

            Assert.That(matchingEvent, Is.EqualTo(nextEvent));
        }
    }
}