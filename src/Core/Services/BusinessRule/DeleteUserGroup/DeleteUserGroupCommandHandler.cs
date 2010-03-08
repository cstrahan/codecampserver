using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup
{
	public class DeleteUserGroupCommandHandler : Command<DeleteUserGroupCommandMessage>
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
		{
			_userGroupRepository = userGroupRepository;
		}

		protected override ReturnValue Execute(DeleteUserGroupCommandMessage commandMessage)
		{
			_userGroupRepository.Delete(commandMessage.UserGroup);
			return new ReturnValue {Type = typeof (UserGroup), Value = commandMessage.UserGroup};
		}
	}
}