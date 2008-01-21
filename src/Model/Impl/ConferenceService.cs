using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;
using CodeCampServer.Model.Security;
using System;
using CodeCampServer.Model.Exceptions;

namespace CodeCampServer.Model.Impl
{
    [Pluggable(Keys.DEFAULT)]
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly ILoginService _loginService;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IAuthenticationService _authenticationService;

        public ConferenceService(IConferenceRepository conferenceRepository, IAttendeeRepository attendeeRepository,
                                 ILoginService loginService, ISpeakerRepository speakerRepository, IAuthenticationService authenticationService)
        {
            _conferenceRepository = conferenceRepository;
            _attendeeRepository = attendeeRepository;
            _loginService = loginService;
            _speakerRepository = speakerRepository;
            _authenticationService = authenticationService;
        }

        public Conference GetConference(string conferenceKey)
        {
            return _conferenceRepository.GetConferenceByKey(conferenceKey);
        }

        public Attendee[] GetAttendees(Conference conference, int page, int perPage)
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

        public Speaker GetSpeakerByDisplayName(string displayName)
        {
            Speaker speaker = _speakerRepository.GetSpeakerByDisplayName(displayName);
            return speaker;
        }

        public IEnumerable<Speaker> GetSpeakers(Conference conference, int page, int perPage)
        {
            return _speakerRepository.GetSpeakersForConference(conference, page, perPage);
        }

        public Speaker GetLoggedInSpeaker()
        {
            string email = GetLoggedInUsername();
            if (!String.IsNullOrEmpty(email))
                return _speakerRepository.GetSpeakerByEmail(email);
            else
                return null;
        }

        public Speaker SaveSpeaker(string emailAddress, string firstName, string lastName, string website, string comment, string displayName, string profile, string avatarUrl)
        {
            Speaker speaker = _speakerRepository.GetSpeakerByEmail(emailAddress);
            
            if (_speakerRepository.CanSaveSpeakerWithDisplayName(speaker, displayName))
            {
                speaker.Contact.FirstName = firstName;
                speaker.Contact.LastName = lastName;
                speaker.Website = website;
                speaker.Comment = comment;
                speaker.DisplayName = displayName;
                speaker.Profile = profile;
                speaker.AvatarUrl = avatarUrl;

                _speakerRepository.Save(speaker);
            }
            else
            {
                throw new DataValidationException("DisplayName is already in use");
            }   

            return speaker;
        }

        public string GetLoggedInUsername()
        {
            return _authenticationService.GetActiveUser();
        }
    }
}
