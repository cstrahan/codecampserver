using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.NHibernate
{
	public class EntityCounterTester : DataTestBase
	{
		[Test]
		public void Should_check_for_uniqueness_on_string_property()
		{
			var address = "foo@example.com";
			var differentCase = "FOO@eXAmple.com";
			var existing = new User {EmailAddress = address};
			PersistEntities(existing);

			var counter = CreateRepository();

			counter.CountByProperty<User>(x => x.EmailAddress, address).ShouldEqual(1);
			counter.CountByProperty<User>(x => x.EmailAddress, differentCase).ShouldEqual(1);
			//Case insensitive.  A SQLServer installation configuration.
			counter.CountByProperty<User>(x => x.EmailAddress, "something else").ShouldEqual(0);
		}

		private EntityCounter CreateRepository()
		{
			return new EntityCounter(new SessionBuilder());
		}
	}
}