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
            Person person = new Person("Andrew","Browne", "");
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
        public void ShouldGetNumberOfUsers()
        {            
            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            Assert.That(repository.GetNumberOfUsers(), Is.EqualTo(0));

            Conference conf = new Conference("test123", "test conference");
            using(var session = getSession())
            {
                session.SaveOrUpdate(conf);
                session.Flush();
            }

            insertTestPerson();
            insertTestPerson();
            insertTestPerson();
            insertTestPerson();
            insertTestPerson();
            
            Assert.That(repository.GetNumberOfUsers(), Is.EqualTo(5));
        }

        private void insertTestPerson()
        {
            using(ISession session = getSession())
            {
                Person p = new Person("test", "person", "person@person.com");
                p.Website = "http://test";
                p.Comment = "test comment";
                session.SaveOrUpdate(p);
                session.Flush();
            }
        }

        [Test]
        public void ShouldFindPersonByEmail()
        {
            insertTestPerson();
            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            Person rehydratedPerson = repository.FindByEmail("person@person.com");
            Assert.That(rehydratedPerson != null);
            Assert.That(rehydratedPerson.Contact.FirstName, Is.EqualTo("test"));
            Assert.That(rehydratedPerson.Contact.LastName, Is.EqualTo("person"));
        }
        [Test]
        public void ShouldNotFindPersonByFullName()
        {
            insertTestPerson();
            IPersonRepository repository = new PersonRepository(_sessionBuilder);
            Person rehydratedPerson = repository.FindByEmail("test person");
            Assert.That(rehydratedPerson == null);
        }
    }
}
