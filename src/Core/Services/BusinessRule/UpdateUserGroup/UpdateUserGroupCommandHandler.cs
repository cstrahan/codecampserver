using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup
{
	public class UpdateUserGroupCommandHandler : Command<UpdateUserGroupCommandMessage>
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public UpdateUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
		{
			_userGroupRepository = userGroupRepository;
		}

		protected override ReturnValue Execute(UpdateUserGroupCommandMessage commandMessage)
		{
			_userGroupRepository.Save(commandMessage.UserGroup);
			return new ReturnValue {Type = typeof (UserGroup), Value = commandMessage.UserGroup};
		}
	}
}