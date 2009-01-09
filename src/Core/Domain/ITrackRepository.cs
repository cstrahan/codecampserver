using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface ITrackRepository : IRepository<Track>
	{
		Track[] GetAllForConference(Conference conference);
	}
}