using System;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class ConferenceForm : EventForm
	{
		public virtual Guid Id { get; set; }

		public virtual Guid UserGroupId { get; set; }

		[BetterValidateNonEmpty("Name")]
		public virtual string Name { get; set; }

		[BetterValidateNonEmpty("Event Key")]
		[ValidateRegExp(@"^[A-Za-z0-9\-]+$", "Key should only contain letters, numbers, and hypens.")]
		public virtual string Key { get; set; }

		[BetterValidateDateTime("Start Date")]
		public override DateTime StartDate { get; set; }

		[BetterValidateDateTime("End Date")]
		public override DateTime EndDate { get; set; }

		[BetterValidateNonEmpty("Time Zone")]
		public override string TimeZone { get; set; }

		[Multiline]
		public virtual string Description { get; set; }

		public virtual bool HasRegistration { get; set; }

		[BetterValidateNonEmpty("Location")]
		public virtual string LocationName { get; set; }

		public virtual string LocationUrl { get; set; }

		public virtual string Address { get; set; }

		public virtual string City { get; set; }

		[Label("State")]
		public virtual string Region { get; set; }

		[Label("Zip Code")]
		public virtual string PostalCode { get; set; }

		[Label("Phone Number")]
		public virtual string PhoneNumber { get; set; }

		[Multiline]
		public string HtmlContent { get; set; }
	}
}