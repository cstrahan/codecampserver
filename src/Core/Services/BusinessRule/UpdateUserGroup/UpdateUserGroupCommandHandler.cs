using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup
{
	public class UpdateUserGroupCommandHandler : ICommand<UpdateUserGroupCommandMessage>
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public UpdateUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
		{
			_userGroupRepository = userGroupRepository;
		}

		public object Execute(UpdateUserGroupCommandMessage commandMessage)
		{
			_userGroupRepository.Save(commandMessage.UserGroup);
			return commandMessage.UserGroup;
		}
	}
}