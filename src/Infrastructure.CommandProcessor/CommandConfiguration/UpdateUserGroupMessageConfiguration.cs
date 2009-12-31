using CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup;
using CodeCampServer.Infrastructure.CommandProcessor.Rules;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class UpdateUserGroupMessageConfiguration : MessageDefinition<UserGroupInput>
	{
		public UpdateUserGroupMessageConfiguration()
		{
			Execute<UpdateUserGroupCommandMessage>()
				.Enforce(expression => expression.Rule<UserGroupKeyMustBeUnique>().RefersTo(i => i.Key))
				;
		}
	}

	public class UpdateSponsorMessageConfiguration : MessageDefinition<SponsorInput>
	{
		public UpdateSponsorMessageConfiguration()
		{
			Execute<UpdateSponsorCommandMessage>();
		}
	}
}