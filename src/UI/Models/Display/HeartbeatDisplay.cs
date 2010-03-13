using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Display
{
	[Label("Heartbeat")]
	public class HeartbeatDisplay
	{
		public virtual string Message { get; set; }
		public virtual string Date { get; set; }
	}
}