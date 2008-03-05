using System;
using System.Collections.Generic;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    [TestFixture]
    public class PersonRepositoryTester : DatabaseTesterBase
    {

        [Test]
        public void ShouldSavePersonToDatabase()
        {
            Conference theConference = new Conference("foo", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.Flush();
            }
            Person person = new Person("Andrew","Browne");
            person.Conference = theConference;
            person.Website = "";
            person.Comment = "";

            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            repository.Save(person);

            Person rehydratedPerson = null;
            //get Person back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedPerson = session.Load<Person>(person.Id);

                Assert.That(rehydratedPerson != null);
                Assert.That(rehydratedPerson.Contact.FirstName, Is.EqualTo("Andrew"));
                Assert.That(rehydratedPerson.Contact.LastName, Is.EqualTo("Browne"));
            }
        }

        [Test]
        public void ShouldSavePersonAndRoleToDatabase()
        {
            Conference theConference = new Conference("foo", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.Flush();
            }
            Person person = new Person("Andrew", "Browne");
            person.Conference = theConference;
            person.Website = "";
            person.Comment = "";

            Role role = new Role();
            person.AddRole(role);

            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            repository.Save(person);

            Person rehydratedPerson = null;
            //get Person back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedPerson = session.Load<Person>(person.Id);

                Assert.That(rehydratedPerson != null);
                Assert.That(rehydratedPerson.RoleCount, Is.EqualTo(1));
            }
        }

        [Test]
        public void ShouldSaveSpeakerToDatabase()
        {
            Conference theConference = new Conference("foo", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.Flush();
            }
            Person person = new Person("Andrew", "Browne");
            person.Conference = theConference;
            person.Website = "";
            person.Comment = "";

            Speaker role = new Speaker();
            role.DisplayName = "Andrew Browne";
            person.AddRole(role);

            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            repository.Save(person);

            Person rehydratedPerson = null;
            //get Person back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedPerson = session.Load<Person>(person.Id);

                Assert.That(rehydratedPerson != null);
                Assert.That(rehydratedPerson.RoleCount, Is.EqualTo(1));
                Assert.That(rehydratedPerson.IsInRole(typeof(Speaker)), Is.True);
                Assert.That(rehydratedPerson.IsInRole(typeof(Attendee)), Is.False);
            }
        }

        [Test]
        public void CanRetrieveSpecificRoleFromDatabase()
        {
            Conference theConference = new Conference("foo", "");
            using (ISession session = getSession())
            {
                session.SaveOrUpdate(theConference);
                session.Flush();
            }
            Person person = new Person("Andrew", "Browne");
            person.Website = "";
            person.Comment = "";
            person.Conference = theConference;

            Speaker role = new Speaker();
            role.DisplayName = "Andrew Browne";
            person.AddRole(role);

            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            repository.Save(person);

            Person rehydratedPerson = null;
            //get Person back from database to ensure it was saved correctly
            using (ISession session = getSession())
            {
                rehydratedPerson = session.Load<Person>(person.Id);

                Assert.That(rehydratedPerson != null);
                Assert.That(rehydratedPerson.IsInRole(typeof(Speaker)), Is.True);
                Speaker Speaker = (rehydratedPerson.GetRole(typeof(Speaker))) as Speaker;
                Assert.That(Speaker != null);
                Assert.That(Speaker.DisplayName == "Andrew Browne");
            }
        }
    }
}