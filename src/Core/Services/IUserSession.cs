using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services
{
	public interface IUserSession
	{
		User GetCurrentUser();
		void LogIn(User user);
		void LogOut();
	}
}