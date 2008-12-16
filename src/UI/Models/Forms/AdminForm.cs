using System;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    public class UserForm
    {
        [BetterValidateNonEmpty("Name")]
        public string Name { get; set; }

        [BetterValidateNonEmpty("Email")]
        public string EmailAddress { get; set; }

        [Hidden]
        public Guid Id { get; set; }

        [BetterValidateNonEmpty("Password")]
        public string Password { get; set; }
    }
}