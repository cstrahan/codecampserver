using CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup;
using CodeCampServer.UI.Models.Messages;
using MvcContrib.CommandProcessor.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class DeleteUserGroupMessageConfiguration : MessageDefinition<DeleteUserGroupInput>
	{
		public DeleteUserGroupMessageConfiguration()
		{
			Execute<DeleteUserGroupCommandMessage>();
		}
	}
}