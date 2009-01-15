using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface ISessionRepository : IKeyedRepository<Session>
	{
		Session[] GetAllForConference(Conference conference);
		Session[] GetAllForTimeSlot(TimeSlot timeSlot);
		Session[] GetAllForTrack(Track track);
		Session[] GetAllForSpeaker(Speaker speaker);
	}
}