using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable("Default")]
	public class ConferenceService : IConferenceService
	{
		private readonly IConferenceRepository _conferenceRepository;
		private readonly IAttendeeRepository _attendeeRepository;

		public ConferenceService(IConferenceRepository conferenceRepository, IAttendeeRepository attendeeRepository)
		{
			_conferenceRepository = conferenceRepository;
			_attendeeRepository = attendeeRepository;
		}
		
		public Conference GetConference(string conferenceKey)
		{
			return _conferenceRepository.GetConferenceByKey(conferenceKey);
		}

		public void RegisterAttendee(Attendee attendee)
		{
			_attendeeRepository.SaveAttendee(attendee);
		}

		public IEnumerable<Attendee> GetAttendees(Conference conference, int page, int perPage)
		{
			return _attendeeRepository.GetAttendeesForConference(conference, page, perPage);
		}
	}
}