using StructureMap;
namespace CodeCampServer.Model.Security
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IAuthenticationService
	{
		void SetActiveUser(string username);
	}
}
