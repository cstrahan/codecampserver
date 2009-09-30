using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	public class SponsorInput
	{
		public virtual Guid? ID { get; set; }

		[Required("Name")]
		public virtual string Name { get; set; }

		[Required("Url")]
		public virtual string Url { get; set; }

		[Required("Banner Url")]
		public virtual string BannerUrl { get; set; }

		public virtual SponsorLevel Level { get; set; }

		public Guid ParentID { get; set; }
	}
}