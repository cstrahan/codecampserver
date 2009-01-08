using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SessionRepositoryTester : DataTestBase
	{
		[Test]
		public void Should_get_by_key()
		{
			var session = new Session {Key = "key"};
			var session2 = new Session {Key = "key1"};
			var session3 = new Session {Key = "key2"};
			var session4 = new Session {Key = "key3"};
			PersistEntities(session, session2, session3, session4);
			ISessionRepository repository = ObjectFactory.GetInstance<SessionRepository>();
			
			Session byKey = repository.GetByKey("key");
			
			byKey.Key.ShouldEqual("key");
		}
	}
}