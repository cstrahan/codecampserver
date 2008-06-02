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
		    _conferenceRepository.Save(conference);
		    return person;
		}

        public Conference GetCurrentConference()
        {
            //try to get the next conference
            var conf = _conferenceRepository.GetFirstConferenceAfterDate(_clock.GetCurrentTime());

            //if there isn't one (or if it isn't public yet), get the most recent one
            if (conf == null || !conf.PubliclyVisible)
                conf = _conferenceRepository.GetMostRecentConference(_clock.GetCurrentTime());

            //if this one isn't public, can't return
            if (conf == null || !conf.PubliclyVisible)
                return null;

            return conf;
        }	  
	}
}