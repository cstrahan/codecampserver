using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
    [Pluggable(Keys.DEFAULT)]
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakerService(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository;
        }

        //TODO:  There is no purpose to these methods.  They are merely pass-throughs.  Clients of these
        //methods should use ISpeakerRepository directly
        public Speaker GetSpeakerByDisplayName(string displayName)
        {
            Speaker speaker = _speakerRepository.GetSpeakerByDisplayName(displayName);
            return speaker;
        }

        public Speaker GetSpeakerByEmail(string emailAddress)
        {
            Speaker speaker = _speakerRepository.GetSpeakerByEmail(emailAddress);
            return speaker;
        }


        public IEnumerable<Speaker> GetSpeakers(Conference conference, int page, int perPage)
        {
            return _speakerRepository.GetSpeakersForConference(conference, page, perPage);
        }

        public Speaker SaveSpeaker(string emailAddress, string firstName, string lastName, string website, string comment,
                                   string displayName, string profile, string avatarUrl)
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
    }
}
