using CodeCampServer.Model;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Website.Helpers
{
    internal class ConferenceServiceHelper
    {
        private readonly IConferenceService _conferenceService;
        private IClock _clock;

        internal ConferenceServiceHelper(IConferenceService conferenceService, IClock clock)
        {
            _conferenceService = conferenceService;
            _clock = clock;
        }

        internal IConferenceService ConferenceService
        {
            get { return _conferenceService; }
        }

        internal IClock Clock
        {
            get { return _clock; }
        }

        internal Schedule GetScheduledConference(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            return new Schedule(conference, _clock, null);
        }
    }
}