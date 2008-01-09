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
		private readonly ILoginService _loginService;

		public ConferenceService(IConferenceRepository conferenceRepository, IAttendeeRepository attendeeRepository,
		                         ILoginService loginService)
		{
			_conferenceRepository = conferenceRepository;
			_attendeeRepository = attendeeRepository;
			_loginService = loginService;
		}

		public Conference GetConference(string conferenceKey)
		{
			return _conferenceRepository.GetConferenceByKey(conferenceKey);
		}

		public IEnumerable<Attendee> GetAttendees(Conference conference, int page, int perPage)
		{
			return _attendeeRepository.GetAttendeesForConference(conference, page, perPage);
		}

		public Attendee RegisterAttendee(string firstName, string lastName, string website, string comment,
		                                 Conference conference, string emailAddress, string cleartextPassword)
		{
			string passwordSalt = _loginService.CreateSalt();
			string encryptedPassword = _loginService.CreatePasswordHash(cleartextPassword, passwordSalt);

			Attendee attendee = new Attendee(firstName, lastName, website, comment, conference, emailAddress, encryptedPassword,
			                                 passwordSalt);
			_attendeeRepository.Save(attendee);
			return attendee;
		}
	}
}