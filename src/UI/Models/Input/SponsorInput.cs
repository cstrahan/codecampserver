using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	public class SponsorInput : IMessage
	{
		public virtual Guid? ID { get; set; }

		[Required("Name")]
		public virtual string Name { get; set; }

		[Required("Url")]
		public virtual string Url { get; set; }

		[Required("Banner Url")]
		public virtual string BannerUrl { get; set; }

		public virtual SponsorLevel Level { get; set; }

		public Guid UserGroupId { get; set; }
	}
}