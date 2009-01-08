using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IConferenceRepository : IKeyedRepository<Conference>
	{
	}

	public interface ISpeakerRepository : IKeyedRepository<Speaker>
	{
	}
}