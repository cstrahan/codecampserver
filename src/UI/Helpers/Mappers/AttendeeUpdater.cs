using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class AttendeeUpdater : ModelUpdater<Conference, AttendeeForm>, IAttendeeUpdater
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

		protected override Guid GetIdFromMessage(AttendeeForm message)
		{
			return message.ConferenceID;
		}

		protected override void UpdateModel(AttendeeForm message, Conference model)
		{
			Attendee attendee;

			if (MessageRepresentsAnAlreadyAttendingAttendee(message, model))
			{
				attendee = model.GetAttendee(message.AttendeeID.Value);
			}
			else
			{
				attendee = new Attendee();
			}

			attendee.EmailAddress = message.EmailAddress;
			attendee.FirstName = message.FirstName;
			attendee.LastName = message.LastName;
			attendee.Status = message.Status;
			attendee.Webpage = message.Webpage;

			model.AddAttendee(attendee);
		}

		private static bool MessageRepresentsAnAlreadyAttendingAttendee(AttendeeForm message, Conference conference)
		{
			return message.AttendeeID.HasValue && conference.IsAttending(message.AttendeeID.Value);
		}

		protected override UpdateResult<Conference, AttendeeForm> PreValidate(AttendeeForm message)
		{
			Conference conference = _repository.GetById(message.ConferenceID);
			var result = new UpdateResult<Conference, AttendeeForm>(false);

			if (conference == null)
			{
				result.WithMessage(m => m.ConferenceID, "Conference does not exist.");
				return result;
			}

			if (message.AttendeeID == null && conference.IsAttending(message.EmailAddress))
			{
				result.WithMessage(c => c.EmailAddress, "Attended is already registered for this conference.");
				return result;
			}
			return base.PreValidate(message);
		}
	}
}