using CodeCampServer.IntegrationTests.DataAccess;
using CodeCampServer.Model.Domain;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Mappings
{
    [TestFixture]
    public class PersonMappingTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldSaveIsAdministrator()
        {
            Person person = new Person("joe", "blow", "jb@gmail.com");
            person.IsAdministrator = true;

            using(var session = getSession())
            {
                session.SaveOrUpdate(person);
                session.Flush();
            }

            using(var session2 = getSession())
            {
                Person personFromDb = session2.Load<Person>(person.Id);
                Assert.That(personFromDb.IsAdministrator);
            }
        }
    }
}
