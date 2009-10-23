using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup
{
	public class UpdateSponsorCommandMessage : ICommandMessage
	{
		public UserGroup UserGroup { get; set; }
		public Sponsor Sponsor { get; set; }
	}
}