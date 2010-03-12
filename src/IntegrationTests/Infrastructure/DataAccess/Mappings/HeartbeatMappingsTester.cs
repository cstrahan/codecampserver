using System;
using CodeCampServer.Core.Domain.Bases;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	public class HeartbeatMappingsTester : DataTestBase
	{
		[Test]
		public void should_map()
		{
			var heartbeat = new Heartbeat
			                	{
			                		Message = "Kilroy was here",
			                		Date = new DateTime(2008, 3, 4),
			                	};
			AssertObjectCanBePersisted(heartbeat);
		}
	}
}