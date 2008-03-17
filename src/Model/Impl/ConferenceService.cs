using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class ConferenceService : IConferenceService
	{
		private readonly IConferenceRepository _conferenceRepository;
	    private readonly IPersonRepository _personRepository;
	    private readonly ILoginService _loginService;
	    private readonly IClock _clock;

		public ConferenceService(IConferenceRepository conferenceRepository, IPersonRepository personRepository, ILoginService loginService, IClock clock)
		{
			_conferenceRepository = conferenceRepository;
		    _personRepository = personRepository;
		    _loginService = loginService;
		    _clock = clock;
		}

		public Conference GetConference(string conferenceKey)
		{
			return _conferenceRepository.GetConferenceByKey(conferenceKey);
		}

		public IEnumerable<Conference> GetAllConferences()
		{
			return _conferenceRepository.GetAllConferences();
		}

		public Person[] GetAttendees(Conference conference, int page, int perPage)
		{
		    return conference.GetAttendees();
		}
		
		public Person RegisterAttendee(string firstName, string lastName, string emailAddress, string website, string comment, Conference conference, string cleartextPassword)
		{
            var passwordSalt = _loginService.CreateSalt();
			var encryptedPassword = _loginService.CreatePasswordHash(cleartextPassword, passwordSalt);

            Person person = new Person(firstName, lastName, emailAddress);
		    person.Website = website;
		    person.Comment = comment;
		    person.PasswordSalt = passwordSalt;
		    person.Password = encryptedPassword;

            conference.AddAttendee(person);

		    return person;
		}

        public Conference GetCurrentConference()
        {
            //try to get the next conference
            Conference conf = _conferenceRepository.GetFirstConferenceAfterDate(_clock.GetCurrentTime());

            //if there isn't one, get the most recent one
            if (conf == null)
                conf = _conferenceRepository.GetMostRecentConference(_clock.GetCurrentTime());

            return conf;
        }
    }
}