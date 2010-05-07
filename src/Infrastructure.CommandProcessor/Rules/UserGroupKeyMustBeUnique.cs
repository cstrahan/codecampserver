using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Services.BusinessRule;
using MvcContrib.CommandProcessor.Validation;

namespace CodeCampServer.Infrastructure.CommandProcessor.Rules
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
			var entity = _repository.GetByKey(message.UserGroup.Key);
			return entity != null && entity.Id != message.UserGroup.Id;
		}
	}
}