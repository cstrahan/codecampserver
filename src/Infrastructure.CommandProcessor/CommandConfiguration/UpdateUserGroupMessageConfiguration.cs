using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Infrastructure.CommandProcessor.Rules;
using CodeCampServer.UI.Models.Input;
using MvcContrib.CommandProcessor.Configuration;

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
}