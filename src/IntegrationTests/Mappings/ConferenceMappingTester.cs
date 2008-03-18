using System;
using CodeCampServer.IntegrationTests.DataAccess;
using CodeCampServer.Model.Domain;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.Mappings
{
    [TestFixture]
    public class ConferenceMappingTester : DatabaseTesterBase
    {
        private Conference _conference;
        private Person _person1;
        private Person _person2;
        
        public override void  Setup()
        {
 	        base.Setup();

            _person1 = new Person("Jeffrey", "Palermo", "jeffrey@palermo.cc");
            _person2 = new Person("Ben", "Scheirman", "ben@scheirman.com");
            _conference = new Conference("foo", "FOO");
            _conference.AddAttendee(_person1);
            _conference.AddAttendee(_person2);
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(_person1);
                session.SaveOrUpdate(_person2);
                session.SaveOrUpdate(_conference);
                session.Flush();
            }
        }

        [Test]
        public void ShouldPopulateAttendees()
        {            
            ISession session1 = getSession();
            var rehydratedConference = session1.Load<Conference>(_conference.Id);
            Person[] attendees = rehydratedConference.GetAttendees();
            Assert.That(attendees.Length, Is.EqualTo(2));
            Assert.That(attendees, Has.Member(_person1));
            Assert.That(attendees, Has.Member(_person2));
        }

        [Test]
        public void AttendeesShouldBeLazy()
        {
            _conference.AddAttendee(_person1);
            _conference.AddAttendee(_person2);
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(_person1);
                session.SaveOrUpdate(_person2);
                session.SaveOrUpdate(_conference);
                session.Flush();
            }

            ISession session1 = getSession();
            var rehydratedConference = session1.Load<Conference>(_conference.Id);
            NHibernateUtil.Initialize(rehydratedConference);
            session1.Dispose();

            try
            {
                rehydratedConference.GetAttendees();
                Assert.Fail("Should have thrown lazy initialization exception");
            }
            catch(LazyInitializationException)
            {               
            }            
        }

        [Test]
        public void ShouldPopulateSpeakers()
        {
            _conference.AddSpeaker(_person1, "jeffrey-palermo", "a guy in Austin", "http://palermoconsulting.com/me.jpg");
            _conference.AddSpeaker(_person2, "ben-scheirman", "a guy in Houston", "http://flux88.com/me.jpg");

            using(var session = getSession())
            {
                session.SaveOrUpdate(_conference);
                session.Flush();
            }

            var rehydratedConference = getSession().Load<Conference>(_conference.Id);
            Speaker[] speakers = rehydratedConference.GetSpeakers();
            Array.Sort(speakers, delegate(Speaker x, Speaker y) { return x.SpeakerKey.CompareTo(y.SpeakerKey); });
           
            Assert.That(speakers[0].Person, Is.EqualTo(_person2));
            Assert.That(speakers[0].SpeakerKey, Is.EqualTo("ben-scheirman"));
            Assert.That(speakers[0].Bio, Is.EqualTo("a guy in Houston"));
            Assert.That(speakers[0].AvatarUrl, Is.EqualTo("http://flux88.com/me.jpg"));

            Assert.That(speakers[1].Person, Is.EqualTo(_person1));
            Assert.That(speakers[1].SpeakerKey, Is.EqualTo("jeffrey-palermo"));
            Assert.That(speakers[1].Bio, Is.EqualTo("a guy in Austin"));
            Assert.That(speakers[1].AvatarUrl, Is.EqualTo("http://palermoconsulting.com/me.jpg"));
        }
    }
}