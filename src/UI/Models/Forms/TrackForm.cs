using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Messages;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TrackForm : ValueObject<TrackForm>, ITrackMessage
	{
		public Guid Id { get; set; }
		[BetterValidateNonEmpty("Name")]
		public string Name { get; set; }
		public Guid ConferenceId { get; set; }
		public string ConferenceKey { get; set; }
	}
}