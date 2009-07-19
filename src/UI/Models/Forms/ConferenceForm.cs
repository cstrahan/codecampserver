using System;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class ConferenceForm
	{
		[BetterValidateNonEmpty("Conference Key")]
		[ValidateRegExp(@"^[A-Za-z0-9\-]+$", "Key should only contain letters, numbers, and hypens.")]
		public virtual string Key { get; set; }

		public virtual Guid Id { get; set; }

        public virtual Guid UserGroupId { get; set; }

		[BetterValidateNonEmpty("Name")]
		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

		[BetterValidateDateTime("Start Date")]
		public virtual string StartDate { get; set; }

		[BetterValidateDateTime("End Date")]
		public virtual string EndDate { get; set; }

		[BetterValidateNonEmpty("Location")]
		public virtual string LocationName { get; set; }

		public virtual string Address { get; set; }

		public virtual string City { get; set; }

		[Label("State")]
		public virtual string Region { get; set; }

		[Label("Zip Code")]
		public virtual string PostalCode { get; set; }

		[Label("Phone Number")]
		public virtual string PhoneNumber { get; set; }

        public virtual bool HasRegistration { get; set; }        

	    public string HtmlContent { get; set; }

        public string GetDate()
        {
            string start = DateTime.Parse(StartDate).ToString("h:mm");
            string end = DateTime.Parse(EndDate).ToString("h:mm tt");
            string date = DateTime.Parse(StartDate).ToShortDateString();
            return string.Format("{0} {1} - {2}",date, start, end);
        }

	}
}