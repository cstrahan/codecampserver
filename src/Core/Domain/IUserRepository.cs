using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IUserRepository : IRepository<User>
	{
		User GetByUserName(string username);
		User[] GetLikeLastNameStart(string query);
	}
}