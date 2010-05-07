using CodeCampServer.Core.Services.BusinessRule.Login;
using CodeCampServer.UI;
using MvcContrib.CommandProcessor.Configuration;
using MvcContrib.CommandProcessor.Validation.Rules;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class LoginUserConfiguration : MessageDefinition<LoginProxyInput>
	{
		public LoginUserConfiguration()
		{
			Execute<LoginProxyCommandMessage>().Enforce(v =>
			                                           	{
			                                           		v.Rule<ValidateNotNull>(c => c.Username).RefersTo(m => m.Username);
			                                           		v.Rule<ValidateNotNull>(c => c.Password).RefersTo(m => m.Password);
			                                           	});
		}
	}
}