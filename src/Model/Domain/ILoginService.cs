using StructureMap;
namespace CodeCampServer.Model.Domain
{
    [PluginFamily("Default")]
    public interface ILoginService
    {
        bool VerifyAccount(string email, string password);
        string CreatePasswordHash(string pwd, string salt);
        string CreateSalt();
    }
}
