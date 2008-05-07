using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
	public class ConferenceService : IConferenceService
	{
		private readonly IConferenceRepository _conferenceRepository;
	    private readonly ICryptoUtil _cryptoUtil;
	    private readonly IClock _clock;

		public ConferenceService(IConferenceRepository conferenceRepository, ICryptoUtil cryptoUtil, IClock clock)
		{
			_conferenceRepository = conferenceRepository;
		    _cryptoUtil = cryptoUtil;
		    _clock = clock;
		}

		public Person[] GetAttendees(Conference conference, int page, int perPage)
		{
		    return conference.GetAttendees();
		}
		
		public Person RegisterAttendee(string firstName, string lastName, string emailAddress, string website, string comment, Conference conference, string cleartextPassword)
		{
		    var passwordSalt = _cryptoUtil.CreateSalt();
			var encryptedPassword = _cryptoUtil.HashPassword(cleartextPassword, passwordSalt);

            var person = new Person(firstName, lastName, emailAddress)
                             {
                                 Website = website,
                                 Comment = comment,
                                 PasswordSalt = passwordSalt,
                                 Password = encryptedPassword
                             };

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