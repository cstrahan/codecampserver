using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup
{
	public class UpdateSponsorCommandHandler : Command<UpdateSponsorCommandMessage>
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public UpdateSponsorCommandHandler(IUserGroupRepository userGroupRepository)
		{
			_userGroupRepository = userGroupRepository;
		}

		protected override ReturnValue Execute(UpdateSponsorCommandMessage commandMessage)
		{
			var userGroup = commandMessage.UserGroup;
			userGroup.Add(commandMessage.Sponsor);
			_userGroupRepository.Save(userGroup);
			return new ReturnValue {Type = typeof (Sponsor), Value = commandMessage.Sponsor};
		}
	}
}