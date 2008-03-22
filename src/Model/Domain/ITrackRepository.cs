
namespace CodeCampServer.Model.Domain
{
    public interface ITrackRepository
	{
	    void Save(Track session);
        Track[] GetTracksFor(Conference conference);
	}
}
