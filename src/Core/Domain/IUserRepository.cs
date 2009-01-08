using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IUserRepository : IKeyedRepository<User>
	{
		User GetByUserName(string username);
		User[] GetLikeLastNameStart(string query);
	}
}