using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    public class SponsorForm : ValueObject<SponsorForm>
    {
        public virtual Guid? ID { get; set; }

        [BetterValidateNonEmpty("Name")]
        public virtual string Name { get; set; }

        [BetterValidateNonEmpty("Url")]
        public virtual string Url { get; set; }

        [BetterValidateNonEmpty("Banner Url")]
        public virtual string BannerUrl { get; set; }

        public virtual SponsorLevel Level { get; set; }

        public Guid ParentID { get; set; }
    }
}