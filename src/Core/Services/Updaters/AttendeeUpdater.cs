using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.Core.Services.Updaters
{
	public class AttendeeUpdater : ModelUpdater<Conference, IAttendeeMessage>, IAttendeeUpdater
	{
		private readonly IConferenceRepository _repository;

		public AttendeeUpdater(IConferenceRepository repository)
		{
			_repository = repository;
		}

		protected override IRepository<Conference> Repository
		{
			get { return _repository; }
		}

		public new UpdateResult<Conference, IAttendeeMessage> UpdateFromMessage(IAttendeeMessage message)
		{
			Conference conference = _repository.GetById(message.ConferenceID);
			var result = new UpdateResult<Conference, IAttendeeMessage>(false);

			if (conference == null)
			{
				result.WithMessage(m => m.ConferenceID, "Conference does not exist.");
				return result;
			}

			if (message.AttendeeID == null &&
			    conference.Attendees.Where(c => c.EmailAddress == message.EmailAddress).FirstOrDefault() != null)
			{
				result.WithMessage(c => c.EmailAddress, "Attended is already registered for this conference.");
				return result;
			}
			return new UpdateResult<Conference, IAttendeeMessage>(true, conference);
		}

		protected override Guid GetIdFromMessage(IAttendeeMessage message)
		{
			return message.ConferenceID;
		}

		protected override void UpdateModel(IAttendeeMessage message, Conference model)
		{
			var attendee = new Attendee
			               	{
			               		EmailAddress = message.EmailAddress,
			               		FirstName = message.FirstName,
			               		LastName = message.LastName,
			               		Status = message.Status,
			               		Webpage = message.Webpage
			               	};

			model.AddAttendee(attendee);
		}
	}
}