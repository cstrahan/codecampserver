using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	[Label("Heartbeat")]
	public class CreateHeartbeatInput
	{
		public string Message { get; set; }
	}
}