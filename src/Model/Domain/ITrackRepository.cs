using StructureMap;
using System.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    [PluginFamily(Keys.DEFAULT)]
    public interface ITrackRepository
	{
	    void Save(Track session);
        Track[] GetTracksFor(Conference conference);
	}
}
