using NUnit.Framework;
using CodeCampServer.Model.Domain;
using NHibernate;
using CodeCampServer.DataAccess.Impl;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;
using System;
namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class SpeakerRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldRetrieveSpeakerByDisplayName()
        {
            Conference anConference = new Conference("tea party", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }
            string email = "brownie@brownie.com.au";
            string displayName = "AndrewBrowne";
            Speaker speaker =
                new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", anConference,
                             email, displayName, "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");

            ISpeakerRepository repository = new SpeakerRepository(_sessionBuilder);
            repository.Save(speaker);

            Speaker rehydratedSpeaker = null;
            //get Speaker back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedSpeaker = repository.GetSpeakerByDisplayName(displayName);

                Assert.That(rehydratedSpeaker != null);
                Assert.That(rehydratedSpeaker.Contact.FirstName, Is.EqualTo("Andrew"));
                Assert.That(rehydratedSpeaker.Website, Is.EqualTo("http://blog.brownie.com.au"));
                Assert.That(rehydratedSpeaker.Comment, Is.EqualTo("the comment"));
                Assert.That(rehydratedSpeaker.Conference, Is.EqualTo(anConference));
                Assert.That(rehydratedSpeaker.AvatarUrl, Is.EqualTo("http://blog.brownie.com.au/avatar.jpg"));
                Assert.That(rehydratedSpeaker.Profile, Is.EqualTo("Info about how important I am to go here."));
                Assert.That(rehydratedSpeaker.DisplayName, Is.EqualTo(displayName));
            }
        }

        [Test]
        public void ExceptionIfSpeakerDisplayNameIsNotUnique()
        {
            Conference anConference = new Conference("tea party", "");
            Speaker speaker1 = GetSpeaker(anConference);
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }

            using (ISession session = getSession())
            {
                ISpeakerRepository repository = new SpeakerRepository(_sessionBuilder);
                repository.Save(speaker1);

                Speaker speaker2 = GetSpeaker(anConference);

                bool exceptionThrownOnSave = false;

                try
                {
                    repository.Save(speaker2);
                }
                catch
                {
                    exceptionThrownOnSave = true;
                }

                Assert.IsTrue(exceptionThrownOnSave, "Exception should be thrown trying to save a second speaker with the same display name");
            }
        }

        [Test]
        public void EnsureRepositoryAllowsUniqueDisplayNames()
        {
            Conference anConference = new Conference("tea party", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }

            using (ISession session = getSession())
            {
                Speaker speaker = GetSpeaker(anConference);
                ISpeakerRepository repository = new SpeakerRepository(_sessionBuilder);
                bool canSave = repository.CanSaveSpeakerWithDisplayName(speaker, "UpdatedDisplayName");
                Assert.IsTrue(canSave);
            }
        }

        [Test]
        public void EnsureRepositoryRejectsNonUniqueDisplayNames()
        {
            Conference anConference = new Conference("tea party", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }

            using (ISession session = getSession())
            {

                Speaker speaker1 = GetSpeaker(anConference);
                ISpeakerRepository repository = new SpeakerRepository(_sessionBuilder);
                repository.Save(speaker1);

                Speaker speaker2 = GetSpeaker(anConference);
                bool canSave = repository.CanSaveSpeakerWithDisplayName(speaker2, speaker2.DisplayName);

                Assert.IsFalse(canSave);
            }
        }

        private Speaker GetSpeaker(Conference anConference)
        {

            return new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", anConference,
                             "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
        }
        [Test]
        public void ShouldRetrieveSpeakerByEmail()
        {
            Conference anConference = new Conference("tea party", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }
            string email = "brownie@brownie.com.au";
            string displayName = "AndrewBrowne";
            Speaker speaker =
                GetSpeaker(anConference);

            ISpeakerRepository repository = new SpeakerRepository(_sessionBuilder);
            repository.Save(speaker);

            Speaker rehydratedSpeaker = null;
            //get Speaker back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedSpeaker = repository.GetSpeakerByEmail(email);

                Assert.That(rehydratedSpeaker != null);
                Assert.That(rehydratedSpeaker.Contact.FirstName, Is.EqualTo("Andrew"));
                Assert.That(rehydratedSpeaker.Website, Is.EqualTo("http://blog.brownie.com.au"));
                Assert.That(rehydratedSpeaker.Comment, Is.EqualTo("the comment"));
                Assert.That(rehydratedSpeaker.Conference, Is.EqualTo(anConference));
                Assert.That(rehydratedSpeaker.AvatarUrl, Is.EqualTo("http://blog.brownie.com.au/avatar.jpg"));
                Assert.That(rehydratedSpeaker.Profile, Is.EqualTo("Info about how important I am to go here."));
                Assert.That(rehydratedSpeaker.DisplayName, Is.EqualTo(displayName));
            }
        }

        [Test]
        public void ShouldGetAttendeesMatchingConference()
        {
            Conference theConference = new Conference("foo", "");

            Speaker speaker1a =
                            new Speaker("Andrewa", "Browne", "http://blog.brownie.com.aua", "the commenta", theConference,
                                         "brownie@brownie.com.au", "AndrewBrowneA", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
            Speaker speaker1b =
                            new Speaker("Andrewb", "Browne", "http://blog.brownie.com.aub", "the commentb", theConference,
                                         "brownie@brownie.com.au", "AndrewBrowneB", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");

            Conference anotherConference = new Conference("bar", "");

            Speaker speaker2 =
                new Speaker("Some", "Person", "http://blog.brownie.com.au", "the comment", anotherConference,
                 "brownie@brownie.com.au", "SomePerson", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");

            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.SaveOrUpdate(anotherConference);
                session.SaveOrUpdate(speaker1a);
                session.SaveOrUpdate(speaker1b);
                session.SaveOrUpdate(speaker2);
                session.Flush();
            }

            ISpeakerRepository repository = new SpeakerRepository(_sessionBuilder);
            IEnumerable<Speaker> matchingSpeakers = repository.GetSpeakersForConference(theConference, 1, 3);
            List<Speaker> speakers = new List<Speaker>(matchingSpeakers);
            speakers.Sort(delegate(Speaker x, Speaker y) { return x.Contact.FirstName.CompareTo(y.Contact.FirstName); });

            Assert.That(speakers.Count, Is.EqualTo(2));
            Assert.That(speakers[0].Conference, Is.EqualTo(theConference));
            Assert.That(speakers[0].Contact.FirstName, Is.EqualTo("Andrewa"));
            Assert.That(speakers[0].Website, Is.EqualTo("http://blog.brownie.com.aua"));
            Assert.That(speakers[0].Comment, Is.EqualTo("the commenta"));
            Assert.That(speakers[0].DisplayName, Is.EqualTo("AndrewBrowneA"));

            Assert.That(speakers[1].Conference, Is.EqualTo(theConference));
            Assert.That(speakers[1].Contact.FirstName, Is.EqualTo("Andrewb"));
            Assert.That(speakers[1].Website, Is.EqualTo("http://blog.brownie.com.aub"));
            Assert.That(speakers[1].Comment, Is.EqualTo("the commentb"));
            Assert.That(speakers[1].DisplayName, Is.EqualTo("AndrewBrowneB"));
        }
    }
}
