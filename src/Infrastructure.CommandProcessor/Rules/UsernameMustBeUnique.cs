using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.BusinessRule.UpdateUser;
using MvcContrib.CommandProcessor.Validation;

namespace CodeCampServer.Infrastructure.CommandProcessor.Rules
{
	public class UsernameMustBeUnique : ValidationRule<UpdateUserCommandMessage>
	{
		private readonly IUniquenessChecker _uniquenessChecker;

		public UsernameMustBeUnique(IUniquenessChecker uniquenessChecker)
		{
			_uniquenessChecker = uniquenessChecker;
		}

		protected override string IsValidCore(UpdateUserCommandMessage message)
		{
			string value = message.Username;
			var isUnique = _uniquenessChecker.IsUnique<User>(value, x => x.Username);
			return isUnique ? Success() : _uniquenessChecker.BuildFailureMessage<User>(value, x => x.Username);
		}
	}
}