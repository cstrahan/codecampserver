using System;
using System.Collections.Generic;
using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class ConferenceRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldGetAllConferences()
        {
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(new Conference("thekey", "theName"));
                Conference theConference = new Conference("thekey2", "theName2");
                theConference.StartDate = new DateTime(2007, 1, 1, 11, 59, 30);
                theConference.EndDate = new DateTime(2007, 1, 1, 11, 59, 31);
                theConference.SponsorInfo = string.Join("a", new string[4001]);
                theConference.Location.Name = "locationname";
                theConference.Location.Address1 = "locationaddress1";
                theConference.Location.Address2 = "locationaddress2";
                theConference.Location.PhoneNumber = "512-555-5555";
                session.SaveOrUpdate(theConference);

                session.Flush();
            }

            resetSession();

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            IEnumerable<Conference> conferences = repository.GetAllConferences();
            List<Conference> conferenceList = new List<Conference>(conferences);
            conferenceList.Sort(delegate(Conference x, Conference y) { return x.Key.CompareTo(y.Key); });

            Assert.That(conferenceList.Count, Is.EqualTo(2));
            Assert.That(conferenceList[0].Key, Is.EqualTo("thekey"));
            Assert.That(conferenceList[0].Name, Is.EqualTo("theName"));
            Assert.That(conferenceList[0].StartDate, Is.Null);
            Assert.That(conferenceList[0].EndDate, Is.Null);
            Assert.That(conferenceList[0].SponsorInfo, Is.Null);
            Assert.That(conferenceList[0].Location, Is.Null);

            Assert.That(conferenceList[1].Key, Is.EqualTo("thekey2"));
            Assert.That(conferenceList[1].Name, Is.EqualTo("theName2"));
            Assert.That(conferenceList[1].StartDate, Is.EqualTo(DateTime.Parse("1/1/2007, 11:59:30 am")));
            Assert.That(conferenceList[1].EndDate, Is.EqualTo(DateTime.Parse("1/1/2007, 11:59:31 am")));
            Assert.That(conferenceList[1].SponsorInfo.Length, Is.EqualTo(4000));
            Assert.That(conferenceList[1].Location.Name, Is.EqualTo("locationname"));
            Assert.That(conferenceList[1].Location.Address1, Is.EqualTo("locationaddress1"));
            Assert.That(conferenceList[1].Location.Address2, Is.EqualTo("locationaddress2"));
            Assert.That(conferenceList[1].Location.PhoneNumber, Is.EqualTo("512-555-5555"));
        }

        [Test]
        public void GetByKey()
        {
            Conference theConference = new Conference("Frank", "some name");
            Conference conference2 = new Conference("Frank2", "some name2");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.SaveOrUpdate(conference2);
                session.Flush();
            }

            resetSession();

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Conference conferenceSaved = repository.GetConferenceByKey("Frank");

            Assert.That(conferenceSaved, Is.Not.Null);
            Assert.That(conferenceSaved, Is.EqualTo(theConference));
            Assert.That(theConference.Key, Is.EqualTo("Frank"));
            Assert.That(theConference.Name, Is.EqualTo("some name"));
        }

        [Test]
        public void ShouldGetNextConferenceBasedOnDate()
        {
            Conference oldConference = new Conference("2006", "past event");
            oldConference.StartDate = new DateTime(2006, 1, 1);
            Conference nextConference = new Conference("2007", "next event");
            nextConference.StartDate = new DateTime(2007, 5, 5);
            Conference futureConference = new Conference("2008", "future event");
            futureConference.StartDate = new DateTime(2008, 1, 1);

            using (ISession session = getSession())
            {
                session.SaveOrUpdate(oldConference);
                session.SaveOrUpdate(nextConference);
                session.SaveOrUpdate(futureConference);
                session.Flush();
            }

            resetSession();

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Conference matchingConference = repository.GetFirstConferenceAfterDate(new DateTime(2006, 1, 2));

            Assert.That(matchingConference, Is.EqualTo(nextConference));
        }

        [Test]
        public void ShouldGetMostRecentConferenceIfNoNextConferenceIsFound()
        {
            var oldConference = new Conference("2005", "past event");
            oldConference.StartDate = new DateTime(2005, 1, 1);
            oldConference.EndDate = new DateTime(2005, 1, 1);
            var olderConference = new Conference("2004", "older event");
            olderConference.StartDate = new DateTime(2004, 5, 5);
            olderConference.EndDate = new DateTime(2004, 5, 5);

            using(var session = getSession())
            {
                session.SaveOrUpdate(olderConference);
                session.SaveOrUpdate(oldConference);                
                session.Flush();
            }

            resetSession();

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Conference conf = repository.GetMostRecentConference(new DateTime(2008, 10, 8));

            Assert.That(conf, Is.EqualTo(oldConference));
        }

        [Test]
        public void ShouldbeAbleToSaveAConference()
        {
            Conference conf = GetTestConference();
            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            repository.Save(conf);
            Assert.AreNotEqual(Guid.Empty, conf.Id, "Primary key should have been set");
            using (ISession session = _sessionBuilder.GetSession())
            {
                session.Clear();
                Conference confFromDb = session.Get<Conference>(conf.Id);
                Assert.IsNotNull(confFromDb, "Couldn't find the conference in the database");
            }
        }

        [Test]
        public void ShouldPersistSponsor()
        {
            Conference conf = GetTestConference();
            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Sponsor sponsor = GetSponsor("test");
            conf.AddSponsor(sponsor);
            repository.Save(conf);
            using (ISession session = _sessionBuilder.GetSession())
            {
                session.Clear();
                Conference confFromDb = session.Get<Conference>(conf.Id);
                Sponsor sponsorFromDb = confFromDb.GetSponsor("test");
                Assert.That(sponsorFromDb, Is.EqualTo(sponsor));
            }
        }

        [Test]
        public void ShouldDeleteSponsor()
        {
            Conference conf = GetTestConference();
            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Sponsor sponsorToDelete = GetSponsor("test");
            Sponsor sponsorToKeep = GetSponsor("test2");
            conf.AddSponsor(sponsorToDelete);
            conf.AddSponsor(sponsorToKeep);
            repository.Save(conf);
            using (ISession session = _sessionBuilder.GetSession())
            {
                session.Clear();
                Conference confFromDb = session.Get<Conference>(conf.Id);
                confFromDb.RemoveSponsor(sponsorToDelete);
                repository.Save(confFromDb);
            }
            using (ISession session = _sessionBuilder.GetSession())
            {
                session.Clear();
                Conference confFromDb = session.Get<Conference>(conf.Id);
                List<Sponsor> sponsors = new List<Sponsor>(confFromDb.GetSponsors());
                Assert.That(sponsors.Contains(sponsorToKeep), Is.True);
                Assert.That(sponsors.Contains(sponsorToDelete), Is.False);
            }
        }

        [Test]
        public void CanProperlySaveEditedSponsor()
        {
            Conference conf = GetTestConference();
            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);

            Sponsor sponsor = GetSponsor("test");
            conf.AddSponsor(sponsor);
            repository.Save(conf);

            Sponsor editedSponsor;

            using (ISession session = _sessionBuilder.GetSession())
            {
                session.Clear();
                Conference confToEdit = session.Get<Conference>(conf.Id);
                editedSponsor = confToEdit.GetSponsor("test");
                editedSponsor.Name = "edited";
                editedSponsor.Level = SponsorLevel.Platinum;
                repository.Save(confToEdit);
            }

            using (ISession session = _sessionBuilder.GetSession())
            {
                session.Clear();
                Conference editedConf = session.Get<Conference>(conf.Id);
                List<Sponsor> sponsors = new List<Sponsor>(editedConf.GetSponsors());
                Assert.That(sponsors.Contains(editedSponsor), Is.True);
                Assert.That(sponsors.Contains(sponsor), Is.False);
            }
        }

        [Test]
        public void ConferenceExistsShouldReturnFalseWithNoNameOrKeyMatches()
        {
            string name = "name";
            string key = "key";

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Assert.That(repository.ConferenceExists(name, key), Is.False);
        }

        [Test]
        public void ConferenceExistsShouldReturnTrueWithMatchingKeyOrName()
        {
            string name = "name";
            string key = "key";

            var conf = new Conference(key, name);
            using(var session = getSession())
            {
                session.SaveOrUpdate(conf);
                session.Flush();
            }

            IConferenceRepository repository = new ConferenceRepository(_sessionBuilder);
            Assert.That(repository.ConferenceExists(name, "different"), Is.True);
            Assert.That(repository.ConferenceExists("different", key), Is.True);
            Assert.That(repository.ConferenceExists(name, key), Is.True);
        }

        private static Sponsor GetSponsor(string name)
        {
            return new Sponsor(name, "", "", "", "", "", SponsorLevel.Gold);
        }

        private static Conference GetTestConference()
        {
            Conference conf = new Conference();
            conf.Name = "test code camp";
            conf.Key = "test-key";
            conf.Location.Address1 = "1234 Northwest Freeway";
            conf.Location.City = "Houston";
            conf.Location.Region = "TX";
            conf.Location.PostalCode = "12345";
            conf.StartDate = DateTime.Parse("Dec 12 2007");
            conf.EndDate = DateTime.Parse("Dec 14 2007");
            return conf;
        }
    }
}
