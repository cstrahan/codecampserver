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
    public class AttendeeRepositoryTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldGetAttendeesMatchingConference()
        {
            Conference theConference = new Conference("foo", "");
            Attendee attendee1a = new Attendee("jima", "foo", "http://www.www.coma", "some commenta", theConference, "a@b.com");
            Attendee attendee1b = new Attendee("jimb", "foo", "http://www.www.comb", "some commentb", theConference, "a@b.com");
            Conference anotherConference = new Conference("bar", "");
            Attendee attendee2 = new Attendee("pam", "foo", "http://www.yahoo.com", "comment", anotherConference, "a@b.com");

            using(ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(theConference);
                session.SaveOrUpdate(anotherConference);
                session.SaveOrUpdate(attendee1a);
                session.SaveOrUpdate(attendee1b);
                session.SaveOrUpdate(attendee2);
                session.Flush();
            }

            IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
            IEnumerable<Attendee> matchingAttendees = repository.GetAttendeesForConference(theConference);
            List<Attendee> attendees = new List<Attendee>(matchingAttendees);
            attendees.Sort(delegate(Attendee x, Attendee y) { return x.Contact.FirstName.CompareTo(y.Contact.FirstName); });

            Assert.That(attendees.Count, Is.EqualTo(2));
            Assert.That(attendees[0].Conference, Is.EqualTo(theConference));
            Assert.That(attendees[0].Contact.FirstName, Is.EqualTo("jima"));
            Assert.That(attendees[0].Website, Is.EqualTo("http://www.www.coma"));
            Assert.That(attendees[0].Comment, Is.EqualTo("some commenta"));

            Assert.That(attendees[1].Conference, Is.EqualTo(theConference));
            Assert.That(attendees[1].Contact.FirstName, Is.EqualTo("jimb"));
            Assert.That(attendees[1].Website, Is.EqualTo("http://www.www.comb"));
            Assert.That(attendees[1].Comment, Is.EqualTo("some commentb"));
        }

        [Test]
        public void ShouldSaveAttendeeToDatabase()
        {
            Conference anConference = new Conference("party", "");
            using(ISession session = getSession(Database.Default))
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }

            Attendee attendee = new Attendee("Jeffrey", "Palermo", "http://www.jeffreypalermo.com", "the comment", anConference, "me@jeffreypalermo.com");
            IAttendeeRepository repository = new AttendeeRepository(_sessionBuilder);
            repository.SaveAttendee(attendee);

            Attendee rehydratedAttendee = null;
            //get Attendee back from database to ensure it was saved correctly
            using (ISession session = getSession(Database.Default))
            {
                rehydratedAttendee = session.Load<Attendee>(attendee.Id);

                Assert.That(rehydratedAttendee != null);
                Assert.That(rehydratedAttendee.Contact.FirstName, Is.EqualTo("Jeffrey"));
                Assert.That(rehydratedAttendee.Website, Is.EqualTo("http://www.jeffreypalermo.com"));
                Assert.That(rehydratedAttendee.Comment, Is.EqualTo("the comment"));
                Assert.That(rehydratedAttendee.Conference, Is.EqualTo(anConference));
            }
        }
    }
}