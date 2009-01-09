using System;
using Castle.Components.Validator;
using CodeCampServer.Core.Messages;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    public class ConferenceForm : IConferenceMessage
    {
        [BetterValidateNonEmpty("Conference Key")]
        [ValidateRegExp(@"^[A-Za-z0-9\-]+$", "Key should only contain letters, numbers, and hypens.")]
        public string Key { get; set; }

        public Guid Id { get; set; }

        [BetterValidateNonEmpty("Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [BetterValidateDateTime("Start Date")]
        public string StartDate { get; set; }

        [BetterValidateDateTime("End Date")]
        public string EndDate { get; set; }

        [BetterValidateNonEmpty("Location")]
        public string LocationName { get; set; }

        public string Address { get; set; }
        
        public string City { get; set; }
        
        [Label("State")]
        public string Region { get; set; }
        
        [Label("Zip Code")]
        public string PostalCode { get; set; }

        [Label("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}