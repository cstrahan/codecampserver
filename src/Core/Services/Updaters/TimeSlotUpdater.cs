using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.Updaters.Impl;

namespace CodeCampServer.Core.Services.Updaters
{
	public class TimeSlotUpdater : ModelUpdater<TimeSlot, ITimeSlotMessage>, ITimeSlotUpdater
	{
		private readonly ITimeSlotRepository _repository;
		private readonly IConferenceRepository _conferenceRepository;

		public TimeSlotUpdater(ITimeSlotRepository repository, IConferenceRepository conferenceRepository)
		{
			_repository = repository;
			_conferenceRepository = conferenceRepository;
		}

		protected override IRepository<TimeSlot> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(ITimeSlotMessage message)
		{
			return message.Id;
		}

		protected override UpdateResult<TimeSlot, ITimeSlotMessage> PreValidate(ITimeSlotMessage message)
		{
			var failureResult = new UpdateResult<TimeSlot, ITimeSlotMessage>(false);
			bool messageHasFailedValidation = false;

			DateTime result;
			if (!DateTime.TryParse(message.StartTime, out result))
			{
				messageHasFailedValidation = true;
				failureResult.WithMessage(m => m.StartTime, "Is not a valid Date and Time.");
			}

			if (!DateTime.TryParse(message.EndTime, out result))
			{
				messageHasFailedValidation = true;
				failureResult.WithMessage(m => m.EndTime, "Is not a valid Date and Time.");
			}

			if (messageHasFailedValidation)
				return failureResult;

			return base.PreValidate(message);
		}

		protected override void UpdateModel(ITimeSlotMessage message, TimeSlot model)
		{
			model.StartTime = DateTime.Parse(message.StartTime);
			model.EndTime = DateTime.Parse(message.EndTime);
			model.Conference = _conferenceRepository.GetById(message.ConferenceId);
		}
	}
}