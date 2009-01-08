using System;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.UI.Models.Forms
{
	public class TrackForm : ITrackMessage
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid ConferenceId { get; set; }
	}
}