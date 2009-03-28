namespace CodeCampServer.Core.Domain.Model
{
	public class Sponsor
	{
		public virtual string Name { get; set; }
		public virtual SponsorLevel Level { get; set; }
		public virtual string Url { get; set; }
	}
}