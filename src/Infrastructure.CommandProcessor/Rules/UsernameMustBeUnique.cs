using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Core.Services.Unique;
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
			var uniquenessSpecification = new EntitySpecificationOfGuid<User>
			                              	{
			                              		Value = message.Username,
			                              		PropertyExpression = x => x.Username,
			                              		Id = message.Id,
			                              	};
			var isUnique = _uniquenessChecker.IsUnique(uniquenessSpecification);
			return isUnique ? Success() : _uniquenessChecker.BuildFailureMessage<User>(message.Username, x => x.Username);
		}
	}
}