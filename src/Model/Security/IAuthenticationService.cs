using StructureMap;
namespace CodeCampServer.Model.Security
{
	[PluginFamily("Default")]
	public interface IAuthenticationService
	{
		void SetActiveUser(string username);
	}
}
