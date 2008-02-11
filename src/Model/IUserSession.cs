using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IUserSession
	{
		Attendee GetCurrentUser();
	}
}