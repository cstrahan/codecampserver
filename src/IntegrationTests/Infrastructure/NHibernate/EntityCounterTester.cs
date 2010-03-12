using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.Unique;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.NHibernate
{
	public class EntityCounterTester : DataTestBase
	{
		[Test]
		public void Should_check_for_uniqueness_by_specification()
		{
			var address = "foo@example.com";
			var otherAddress = "other@example.com";
			var differentCase = "FOO@eXAmple.com";
			
			var existing = new User {EmailAddress = address};
			var incoming = new User {EmailAddress = otherAddress};
			
			PersistEntities(existing);

			var counter = CreateEntityCounter();

			//The only existing one is "me"
			var spec = new EntitySpecificationOfGuid<User>{PropertyExpression = x=> x.EmailAddress, Value = address, Id = existing.Id};
			counter.CountByProperty(spec).ShouldEqual(0);
			
			//The existing user has this value
			var spec2 = new EntitySpecificationOfGuid<User>{PropertyExpression = x=> x.EmailAddress, Value = address, Id = incoming.Id};
			counter.CountByProperty(spec2).ShouldEqual(1);
			
			//Case insensitive.  A SQLServer installation configuration.
			var spec3 = new EntitySpecificationOfGuid<User> { PropertyExpression = x => x.EmailAddress, Value = differentCase, Id = incoming.Id };
			counter.CountByProperty(spec3).ShouldEqual(1);

			//This email address is not in the database
			var spec4 = new EntitySpecificationOfGuid<User> { PropertyExpression = x => x.EmailAddress, Value = otherAddress, Id = incoming.Id };
			counter.CountByProperty(spec4).ShouldEqual(0);
		}

		private EntityCounter CreateEntityCounter()
		{
			return new EntityCounter(new SessionBuilder());
		}
	}
}