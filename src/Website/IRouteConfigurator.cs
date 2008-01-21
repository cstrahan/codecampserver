using CodeCampServer.Model;
using StructureMap;

namespace CodeCampServer.Website
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IRouteConfigurator
	{
		void RegisterRoutes();
	}
}