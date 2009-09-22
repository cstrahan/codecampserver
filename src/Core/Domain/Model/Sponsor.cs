
namespace CodeCampServer.Core.Domain.Model
{
	public class Sponsor:PersistentObject 
	{
		public virtual string Name { get; set; }
		public virtual SponsorLevel Level { get; set; }
		public virtual string Url { get; set; }
        public virtual string BannerUrl { get; set; }
	}
}