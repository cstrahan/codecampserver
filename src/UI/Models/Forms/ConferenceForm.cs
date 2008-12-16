using System;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    public class ConferenceForm
    {
        public Guid Id { get; set; }

        [BetterValidateNonEmpty("Name")]
        public string Name { get; set; }

        [BetterValidateDateTime("Start Date")]
        public string StartDate { get; set; }

        [BetterValidateDateTime("End Date")]
        public string EndDate { get; set; }
    }
}