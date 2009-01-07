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
	public class PotentialAttendeeRepositoryTester : DataTestBase
	{
		[Test]
		public void should_find_all_attendees()
		{
			var attendee = new PotentialAttendee() {};
            var attendee1 = new PotentialAttendee { };

			using (var session = GetSession())
			{
				session.SaveOrUpdate(attendee);
				session.SaveOrUpdate(attendee1);
				session.Flush();
			}

			var repos = ObjectFactory.GetInstance<IPotentialAttendeeRepository>();

			var persistedUsers = repos.GetAll();
			CollectionAssert.AreEquivalent(new[] {attendee, attendee1}, persistedUsers);
		}


		[Test]
		public void Should_save_user()
		{
			var attendee = new PotentialAttendee()
			           	{
			           		
			           		EmailAddress = "user@example.com",
                            
                            Name = "admin"
			           	};


			var repository = ObjectFactory.GetInstance<IPotentialAttendeeRepository>();
			repository.Save(attendee);

			using (ISession session = GetSession())
			{
				var rehydratedUser = session.Load<PotentialAttendee>(attendee.Id);

				Assert.That(rehydratedUser.Id, Is.EqualTo(attendee.Id));
				Assert.That(rehydratedUser.EmailAddress, Is.EqualTo(attendee.EmailAddress));
				
                Assert.That(rehydratedUser.Name, Is.EqualTo(attendee.Name));
                
            }
		}

		[Test]
		public void Should_get_by_id()
		{
			var attendee = new PotentialAttendee();

			using (var session = GetSession())
			{
				session.Save(attendee);
				session.Flush();
			}

            var repository = ObjectFactory.GetInstance<IPotentialAttendeeRepository>();
			var attendee1 = repository.GetById(attendee.Id);
			Assert.That(attendee1, Is.EqualTo(attendee));
		}

	}
}