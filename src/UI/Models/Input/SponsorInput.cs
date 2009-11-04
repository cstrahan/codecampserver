using System;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	public class SponsorInput
	{
		public virtual Guid ID { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		public virtual string Url { get; set; }

		[Required]
		[Label("Banner Url")]
		public virtual string BannerUrl { get; set; }

		public virtual SponsorLevel Level { get; set; }

		public Guid UserGroupId { get; set; }
	}
}