using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup
{
	public class DeleteUserGroupCommandMessage
	{
		public UserGroup UserGroup { get; set; }
	}
}