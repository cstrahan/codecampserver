using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class TimeSlotMapper : AutoFormMapper<TimeSlot, TimeSlotForm>, ITimeSlotMapper
	{
		private readonly IConferenceRepository _conferenceRepository;

		public TimeSlotMapper(ITimeSlotRepository repository, IConferenceRepository conferenceRepository) : base(repository)
		{
			_conferenceRepository = conferenceRepository;
		}

		protected override Guid GetIdFromMessage(TimeSlotForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(TimeSlotForm form, TimeSlot model)
		{
			model.StartTime = DateTime.Parse(form.StartTime);
			model.EndTime = DateTime.Parse(form.EndTime);
			model.Conference = _conferenceRepository.GetById(form.ConferenceId);
		}

		public TimeSlotForm[] Map(TimeSlot[] timeSlots)
		{
			return AutoMapper.Mapper.Map<TimeSlot[], TimeSlotForm[]>(timeSlots);
		}
	}
}