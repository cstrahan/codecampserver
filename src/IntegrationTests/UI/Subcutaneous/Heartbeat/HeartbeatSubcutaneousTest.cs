using CodeCampServer.UI.Models.Input;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.UI.Subcutaneous.Heartbeat
{
	[TestFixture]
	public class HeartbeatSubcutaneousTest : SubcutaneousTest<HeartbeatInput>
	{
		[Test]
		public void Should_process_new_heartbeat()
		{
			var input = new HeartbeatInput
			            	{
			            		Message = "Can haz hartbeet?  Kthxbye!"
			            	};

			var result = ProcessForm(input);

			result.ShouldBeSuccessful();
		}
	}
}