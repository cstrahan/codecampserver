using System;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	public class SponsorInput : IMessage
	{
		public virtual Guid? ID { get; set; }

		[Required()]
		public virtual string Name { get; set; }

		[Required()]
		public virtual string Url { get; set; }

		[Required()]
		[Label("Banner Url")]
		public virtual string BannerUrl { get; set; }

		public virtual SponsorLevel Level { get; set; }

		public Guid UserGroupId { get; set; }
	}
}