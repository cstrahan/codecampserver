using CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.BusinessRules
{
	public class DeleteUserGroupMessageConfiguration : MessageDefinition<DeleteUserGroupInput>
	{

		public DeleteUserGroupMessageConfiguration()
		{
			Execute<DeleteUserGroupCommandMessage>();
		}		
	}
}