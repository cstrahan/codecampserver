using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;

namespace CodeCampServer.Core.Services.Updaters
{
    public class ConferneceUpdater : IConferneceUpdater
    {
        private readonly IConferenceRepository _repository;

        public ConferneceUpdater(IConferenceRepository repository)
        {
            _repository = repository;
                
        }

        public UpdateResult<Conference, IAttendeeMessage> UpdateFromMessage(IAttendeeMessage message)
        {
            var conference = _repository.GetById(message.ConferenceID);
            var result = new UpdateResult<Conference, IAttendeeMessage>(false);
                
            if (conference == null)
            {
                result.WithMessage(m => m.ConferenceID, "Conference does not exist.");
                return result;
            }
                
            if(message.AttendeeID==null && conference.Attendees.Where(c=>c.EmailAddress==message.EmailAddress).FirstOrDefault()!=null)
            {                    
                result.WithMessage(c => c.EmailAddress, "Attended is already registered for this conference.");
                return result;
            }
            return new UpdateResult<Conference, IAttendeeMessage>(true,conference);
        }
    }
}