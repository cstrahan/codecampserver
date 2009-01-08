using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services
{
	public interface IAuthenticationService
	{
		bool PasswordMatches(User user, string password);
	}
}