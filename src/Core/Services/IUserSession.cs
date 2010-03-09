using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Services
{
	public interface IUserSession
	{
		User GetCurrentUser();
		void LogIn(User user);
		void LogOut();
	}
}