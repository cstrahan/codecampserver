using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IConferenceRepository : IKeyedRepository<Conference>
	{
		Conference GetNextConference();
	}
}