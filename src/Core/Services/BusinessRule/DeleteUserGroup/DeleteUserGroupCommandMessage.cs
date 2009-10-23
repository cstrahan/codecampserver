using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup
{
	public class DeleteUserGroupCommandMessage : ICommandMessage
	{
		public UserGroup UserGroup { get; set; }
	}
}