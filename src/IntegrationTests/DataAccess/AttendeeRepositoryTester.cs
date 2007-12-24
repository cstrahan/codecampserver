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
    public class AttendeeRepositoryTester : RepositoryBase
    {
        [Test]
        public void ShouldGetAttendeesMatchingEvent()
        {
            TestHelper.EmptyDatabase();
            Conference theConference = new Conference("foo", "");
            Attendee attendee1a = new Attendee("jima", "http://www.www.coma", "some commenta", theConference);
            Attendee attendee1b = new Attendee("jimb", "http://www.www.comb", "some commentb", theConference);
            Conference anotherConference = new Conference("bar", "");
            Attendee attendee2 = new Attendee("pam", "http://www.yahoo.com", "comment", anotherConference);

            using(ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.SaveOrUpdate(anotherConference);
                session.SaveOrUpdate(attendee1a);
                session.SaveOrUpdate(attendee1b);
                session.SaveOrUpdate(attendee2);
                session.Flush();
            }

            IAttendeeRepository repository = new AttendeeRepository();
            IEnumerable<Attendee> matchingAttendees = repository.GetAttendeesForEvent(theConference);
            List<Attendee> attendees = new List<Attendee>(matchingAttendees);
            attendees.Sort(delegate(Attendee x, Attendee y) { return x.Name.CompareTo(y.Name); });

            Assert.That(attendees.Count, Is.EqualTo(2));
            Assert.That(attendees[0].Conference, Is.EqualTo(theConference));
            Assert.That(attendees[0].Name, Is.EqualTo("jima"));
            Assert.That(attendees[0].Website, Is.EqualTo("http://www.www.coma"));
            Assert.That(attendees[0].Comment, Is.EqualTo("some commenta"));

            Assert.That(attendees[1].Conference, Is.EqualTo(theConference));
            Assert.That(attendees[1].Name, Is.EqualTo("jimb"));
            Assert.That(attendees[1].Website, Is.EqualTo("http://www.www.comb"));
            Assert.That(attendees[1].Comment, Is.EqualTo("some commentb"));
        }

        [Test]
        public void ShouldSaveAttendeeToDatabase()
        {
            TestHelper.EmptyDatabase();
            Conference anConference = new Conference("party", "");
            using(ISession session = getSession())
            {
                session.SaveOrUpdate(anConference);
                session.Flush();
            }

            Attendee attendee = new Attendee("Jeffrey Palermo", "http://www.jeffreypalermo.com", "the comment", anConference);
            IAttendeeRepository repository = new AttendeeRepository();
            repository.SaveAttendee(attendee);

            Attendee rehydratedAttendee = null;
            //get Attendee back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedAttendee = session.Load<Attendee>(attendee.Id);

                Assert.That(rehydratedAttendee != null);
                Assert.That(rehydratedAttendee.Name, Is.EqualTo("Jeffrey Palermo"));
                Assert.That(rehydratedAttendee.Website, Is.EqualTo("http://www.jeffreypalermo.com"));
                Assert.That(rehydratedAttendee.Comment, Is.EqualTo("the comment"));
                Assert.That(rehydratedAttendee.Conference, Is.EqualTo(anConference));
            }
        }
    }
}