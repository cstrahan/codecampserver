using StructureMap;
namespace CodeCampServer.Model.Domain
{
	[PluginFamily(Keys.DEFAULT)]
    public interface ILoginService
    {
        bool VerifyAccount(string email, string password);
        string CreatePasswordHash(string pwd, string salt);
        string CreateSalt();
    }
}
