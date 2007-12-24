using System;
using System.Collections.Generic;
using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Domain;
using CodeCampServer.Domain.Model;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class ConferenceRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldGetAllEvents()
        {
            TestHelper.EmptyDatabase();
            using (ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(new Conference("thekey", "theName"));
                Conference theConference = new Conference("thekey2", "theName2");
                theConference.StartDate = new DateTime(2007, 1, 1, 11, 59, 30);
                theConference.EndDate = new DateTime(2007, 1, 1, 11, 59, 31);
                theConference.SponsorInfo = string.Join("a", new string[100001]);
                theConference.Location.Name = "locationname";
                theConference.Location.Address1 = "locationaddress1";
                theConference.Location.Address2 = "locationaddress2";
                theConference.Location.PhoneNumber = "512-555-5555";
                session.SaveOrUpdate(theConference);

                session.Flush();
            }

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            IEnumerable<Conference> events = repository.GetAllEvents();
            List<Conference> eventList = new List<Conference>(events);
            eventList.Sort(delegate(Conference x, Conference y) { return x.Key.CompareTo(y.Key); });

            Assert.That(eventList.Count, Is.EqualTo(2));
            Assert.That(eventList[0].Key, Is.EqualTo("thekey"));
            Assert.That(eventList[0].Name, Is.EqualTo("theName"));
            Assert.That(eventList[0].StartDate, Is.Null);
            Assert.That(eventList[0].EndDate, Is.Null);
            Assert.That(eventList[0].SponsorInfo, Is.Null);
            Assert.That(eventList[0].Location, Is.Null);

            Assert.That(eventList[1].Key, Is.EqualTo("thekey2"));
            Assert.That(eventList[1].Name, Is.EqualTo("theName2"));
            Assert.That(eventList[1].StartDate, Is.EqualTo(DateTime.Parse("1/1/2007, 11:59:30 am")));
            Assert.That(eventList[1].EndDate, Is.EqualTo(DateTime.Parse("1/1/2007, 11:59:31 am")));
            Assert.That(eventList[1].SponsorInfo.Length, Is.EqualTo(100000));
            Assert.That(eventList[1].Location.Name, Is.EqualTo("locationname"));
            Assert.That(eventList[1].Location.Address1, Is.EqualTo("locationaddress1"));
            Assert.That(eventList[1].Location.Address2, Is.EqualTo("locationaddress2"));
            Assert.That(eventList[1].Location.PhoneNumber, Is.EqualTo("512-555-5555"));

        }

        [Test]
        public void GetByKey()
        {
            TestHelper.EmptyDatabase();
            Conference theConference = new Conference("Frank", "some name");
            Conference theEvent2 = new Conference("Frank2", "some name2");
            using (ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(theConference);
                session.SaveOrUpdate(theEvent2);
                session.Flush();
            }

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Conference conferenceSaved = repository.GetEventByKey("Frank");

            Assert.That(conferenceSaved, Is.Not.Null);
            Assert.That(conferenceSaved, Is.EqualTo(theConference));
            Assert.That(theConference.Key, Is.EqualTo("Frank"));
            Assert.That(theConference.Name, Is.EqualTo("some name"));
        }

        [Test]
        public void ShouldGetNextEventBasedOnDate()
        {
            TestHelper.EmptyDatabase();
            Conference oldConference = new Conference("2006", "past event");
            oldConference.StartDate = new DateTime(2006, 1, 1);
            Conference nextConference = new Conference("2007", "next event");
            nextConference.StartDate = new DateTime(2007, 5, 5);
            Conference futureConference = new Conference("2008", "future event");
            futureConference.StartDate = new DateTime(2008, 1, 1);

            using(ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(oldConference);
                session.SaveOrUpdate(nextConference);
                session.SaveOrUpdate(futureConference);
                session.Flush();
            }

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Conference matchingConference = repository.GetFirstEventAfterDate(new DateTime(2006, 1, 2));

            Assert.That(matchingConference, Is.EqualTo(nextConference));
        }
    }
}