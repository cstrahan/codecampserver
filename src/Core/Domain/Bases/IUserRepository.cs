namespace CodeCampServer.Core.Domain.Bases
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(string username);
        User[] GetLikeLastNameStart(string query);
    }
}