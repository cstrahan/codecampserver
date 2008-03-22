
namespace CodeCampServer.Model.Domain
{
    public interface ILoginService
    {
        bool VerifyAccount(string email, string password);
        string CreatePasswordHash(string pwd, string salt);
        string CreateSalt();
    }
}
