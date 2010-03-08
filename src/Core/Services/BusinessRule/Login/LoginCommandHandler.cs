using CodeCampServer.Core.Domain.Bases;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;

namespace CodeCampServer.Core.Services.BusinessRule.Login
{
	public class LoginCommandHandler : Command<LoginUserCommandMessage>
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IUserRepository _repository;

		public LoginCommandHandler(IAuthenticationService authenticationService,
		                           IUserRepository repository)
		{
			_authenticationService = authenticationService;
			_repository = repository;
		}

		protected override ReturnValue Execute(LoginUserCommandMessage commandMessage)
		{
			var user = _repository.GetByUserName(commandMessage.Username);
			if (user != null)
			{
				if (_authenticationService.PasswordMatches(user, commandMessage.Password))
				{
					return new ReturnValue {Type = typeof (User), Value = user};
				}
			}
			return new ReturnValue {Type = typeof (User), Value = null};
		}
	}
}