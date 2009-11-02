using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup
{
	public class UpdateSponsorCommandMessage 
	{
		public UserGroup UserGroup { get; set; }
		public Sponsor Sponsor { get; set; }
	}
}