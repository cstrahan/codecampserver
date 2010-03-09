using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Services
{
	public interface IAuthenticationService
	{
		bool PasswordMatches(User user, string password);
	}
}