using System;
using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Display
{
	[Label("Heartbeat")]
	public class HeartbeatDisplay
	{
		public string Message { get; set; }
		public string Date { get; set; }
		public Guid Id { get; set; }
	}
}