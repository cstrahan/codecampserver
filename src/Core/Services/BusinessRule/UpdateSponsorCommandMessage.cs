using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Core.Services.BusinessRule
{
	public class UpdateSponsorCommandMessage
	{
		public virtual Sponsor Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Url { get; set; }
		public virtual string BannerUrl { get; set; }
		public virtual SponsorLevel Level { get; set; }
		public virtual UserGroup UserGroup { get; set; }
	}
}