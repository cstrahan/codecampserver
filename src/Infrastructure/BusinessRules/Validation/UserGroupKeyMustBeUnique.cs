using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup;
using Tarantino.RulesEngine;

namespace CodeCampServer.Infrastructure.BusinessRules.Validation
{
	public class UserGroupKeyMustBeUnique : IValidationRule
	{
		private readonly IUserGroupRepository _repository;

		public UserGroupKeyMustBeUnique(IUserGroupRepository repository)
		{
			_repository = repository;
		}

		public bool StopProcessing
		{
			get { return false; }
		}

		public string IsValid(object input)
		{
			return UserGroupKeyAlreadyExists((UpdateUserGroupCommandMessage) input) ? "The key must be unique." : null;
		}

		private bool UserGroupKeyAlreadyExists(UpdateUserGroupCommandMessage message)
		{
			UserGroup entity = _repository.GetByKey(message.UserGroup.Key);
			return entity != null && entity.Id != message.UserGroup.Id;
		}
	}
}