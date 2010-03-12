using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	public class HeartbeatMappingsTester : DataTestBase
	{
		[Test]
		public void should_map()
		{
			var currentUser = new User();
			PersistEntities(currentUser);

			ObjectFactory.Inject<IUserSession>(new UserSessionStub(currentUser));

			var heartbeat = new Heartbeat
			                	{
			                		Message = "Kilroy was here",
			                	};
			AssertObjectCanBePersisted(heartbeat);
		}
	}
}