using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.UnitTests;
using NHibernate;
using NHibernate.Metadata;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SessionFactoryBuilderTester : TestBase
	{
		[Test]
		public void Should_create_the_NHibernate_configuration()
		{
			var builder = new SessionFactoryBuilder();
			ISessionFactory sessionFactory = builder.GetFactory();
			sessionFactory.ShouldNotBeNull();
			IClassMetadata metadata = sessionFactory.GetClassMetadata(typeof(User));
			metadata.EntityName.Contains(typeof(User).Name).ShouldBeTrue();
		}

		[Test]
		public void Should_store_session_factory_in_singleton()
		{
			var builder = new SessionFactoryBuilder();
			ISessionFactory factory1 = builder.GetFactory();
			factory1.ShouldNotBeNull();

			ISessionFactory factory2 = builder.GetFactory();
			ReferenceEquals(factory1, factory2).ShouldBeTrue();
		}
	}
}