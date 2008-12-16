using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class ConferenceRepositoryTester : DataTestBase
	{
		[Test]
		public void should_find_all_conferences()
		{
            var conference = new Conference { StartDate = new DateTime(2008, 12, 1), EndDate = new DateTime(2008, 12, 1) };
            var conference1 = new Conference { StartDate = new DateTime(2008, 12, 1), EndDate = new DateTime(2008, 12, 1) };

			using (var session = GetSession())
			{
				session.SaveOrUpdate(conference);
				session.SaveOrUpdate(conference1);
				session.Flush();
			}

			var repos = ObjectFactory.GetInstance<IConferenceRepository>();

			var persistedUsers = repos.GetAll();
			CollectionAssert.AreEquivalent(new[] {conference, conference1}, persistedUsers);
		}

		[Test]
		public void Should_save_conference()
		{
            Conference conference = CreateConference();

			var repository = ObjectFactory.GetInstance<IConferenceRepository>();
			repository.Save(conference);

			using (ISession session = GetSession())
			{
                var rehydratedConference = session.Load<Conference>(conference.Id);

				Assert.That(rehydratedConference.Id, Is.EqualTo(conference.Id));
				Assert.That(rehydratedConference.Name, Is.EqualTo(conference.Name));
				Assert.That(rehydratedConference.Description, Is.EqualTo(conference.Description));
                Assert.That(rehydratedConference.LocationName, Is.EqualTo(conference.LocationName));
                Assert.That(rehydratedConference.StartDate, Is.EqualTo(conference.StartDate));
            }
		}

	    private Conference CreateConference()
	    {
	        return new Conference()
	                   {
	                       Name = "sdf",
	                       Description = "description",
	                       StartDate = new DateTime(2008, 12, 2),
	                       EndDate = new DateTime(2008, 12, 3),
	                       LocationName = "St Edwards Professional Education Center",
	                       Address = "12343 Research Blvd",
	                       City = "Austin",
	                       Region = "Tx",
	                       PostalCode = "78234",
	                       PhoneNumber = "512-555-1234"

	                   };
	    }

	    [Test]
		public void Should_get_by_id()
		{
			var conference = CreateConference();

			using (var session = GetSession())
			{
				session.Save(conference);
				session.Flush();
			}

			var repository = ObjectFactory.GetInstance<IConferenceRepository>();
			var conference1 = repository.GetById(conference.Id);
			Assert.That(conference1, Is.EqualTo(conference));
		}
	}
}