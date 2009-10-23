using CodeCampServer.Core.Services.BusinessRule.Login;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;
using Tarantino.RulesEngine.ValidationRules;

namespace CodeCampServer.Infrastructure.BusinessRules.CommandConfiguration
{
	public class LoginUserConfiguration : MessageDefinition<LoginInput>
	{
		public LoginUserConfiguration()
		{
			Execute<LoginUserCommandMessage>().Enforce(
				v =>
					{
						v.Rule<ValidateNotNull>(c => c.Username).RefersTo(m => m.Username);
						v.Rule<ValidateNotNull>(c => c.Password).RefersTo(m => m.Password);
					});
		}
	}
}