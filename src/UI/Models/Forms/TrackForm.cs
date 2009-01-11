using System;
using CodeCampServer.Core;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Forms.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TrackForm : ValueObject<TrackForm>, ITrackMessage
	{
		public Guid Id { get; set; }

		[BetterValidateNonEmpty("Name")]
		public string Name { get; set; }

		public Guid ConferenceId { get; set; }
		
		[Hidden]
		public string ConferenceKey { get; set; }
	}
}