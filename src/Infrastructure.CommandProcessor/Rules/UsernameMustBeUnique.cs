using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.BusinessRule.UpdateUser;
using MvcContrib.CommandProcessor.Validation;

namespace CodeCampServer.Infrastructure.CommandProcessor.Rules
{
	public class UsernameMustBeUnique : IValidationRule
	{
		private readonly IUserRepository _repository;

		public UsernameMustBeUnique(IUserRepository repository)
		{
			_repository = repository;
		}

		public bool StopProcessing
		{
			get { return false; }
		}

		public string IsValid(object input)
		{
			return UsernameAlreadyExists((UpdateUserCommandMessage) input) ? "Username is already taken." : null;
		}

		private bool UsernameAlreadyExists(UpdateUserCommandMessage message)
		{
			var entity = _repository.GetByUserName(message.Username);
			return entity != null && !Equals(entity.Id, message.Id);
		}
	}
}