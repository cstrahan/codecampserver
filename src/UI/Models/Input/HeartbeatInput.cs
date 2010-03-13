using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	[Label("Heartbeat")]
	public class HeartbeatInput
	{
		public string Message { get; set; }
	}
}