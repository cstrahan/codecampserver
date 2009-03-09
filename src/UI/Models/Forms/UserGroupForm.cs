using System;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    public class UserGroupForm
    {
        [BetterValidateNonEmpty("User Group Key")]
        [ValidateRegExp(@"^[A-Za-z0-9\-]+$", "Key should only contain letters, numbers, and hypens.")]
        public virtual string Key { get; set; }

        public virtual Guid Id { get; set; }

        [BetterValidateNonEmpty("Name")]
        public virtual string Name { get; set; }

        public virtual string HomepageHTML { get; set; }

        public virtual string City { get; set; }

        public virtual string Region { get; set; }

        public virtual string Country { get; set; }

        public virtual string DomainName { get; set; }

        public virtual string GoogleAnalysticsCode { get; set; }
    }
}