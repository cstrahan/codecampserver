using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.UnitTests;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SessionFactoryBuilderTester : TestBase
	{
		[Test]
		public void Should_create_the_NHibernate_configuration()
		{
			var builder = new SessionFactoryBuilder();
			var sessionFactory = builder.GetFactory();
			sessionFactory.ShouldNotBeNull();
			var metadata = sessionFactory.GetClassMetadata(typeof (User));
			metadata.EntityName.Contains(typeof (User).Name).ShouldBeTrue();
		}

		[Test]
		public void Should_store_session_factory_in_singleton()
		{
			var builder = new SessionFactoryBuilder();
			var factory1 = builder.GetFactory();
			factory1.ShouldNotBeNull();

			var factory2 = builder.GetFactory();
			ReferenceEquals(factory1, factory2).ShouldBeTrue();
		}
	}
}