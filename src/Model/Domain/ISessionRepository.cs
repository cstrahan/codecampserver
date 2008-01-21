using StructureMap;

namespace CodeCampServer.Model.Domain
{
    [PluginFamily(Keys.DEFAULT)]
    public interface ISessionRepository
	{
	    void Save(Session session);
	}
}
