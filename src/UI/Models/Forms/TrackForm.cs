using System;
using CodeCampServer.Core;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Forms.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TrackForm : ValueObject<TrackForm>
	{
		public virtual Guid Id { get; set; }

		[BetterValidateNonEmpty("Name")]
		public virtual string Name { get; set; }

		public virtual Guid ConferenceId { get; set; }

		[Hidden]
		public virtual string ConferenceKey { get; set; }
	}
}