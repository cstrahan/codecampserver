using System;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Core.Domain.Model
{
	public class Sponsor : AuditedPersistentObject
	{
		public virtual string Name { get; set; }
		public virtual SponsorLevel Level { get; set; }
		public virtual string Url { get; set; }
		public virtual string BannerUrl { get; set; }

		public virtual UserGroup UserGroup { get; set; }
	}
}