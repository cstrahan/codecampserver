using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class SessionMapper : FormMapper<Session, SessionForm>, ISessionMapper
	{
		private readonly ITrackRepository _trackRepository;
		private readonly ITimeSlotRepository _timeSlotRepository;
		private readonly ISpeakerRepository _speakerRepository;

		public SessionMapper(ISessionRepository repository, ITrackRepository trackRepository,
		                     ITimeSlotRepository timeSlotRepository, ISpeakerRepository speakerRepository) : base(repository)
		{
			_trackRepository = trackRepository;
			_timeSlotRepository = timeSlotRepository;
			_speakerRepository = speakerRepository;
		}

		protected override Guid GetIdFromMessage(SessionForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(SessionForm form, Session model)
		{
			model.Track = _trackRepository.GetById(form.Track.Id);
			model.TimeSlot = _timeSlotRepository.GetById(form.TimeSlot.Id);
			model.Speaker = _speakerRepository.GetById(form.Speaker.Id);
			model.Conference = form.Conference;
			model.RoomNumber = form.RoomNumber;
			model.Title = form.Title;
			model.Abstract = form.Abstract;
			model.Level = form.Level;
			model.MaterialsUrl = form.MaterialsUrl;
			model.Key = form.Key;
		}

		public SessionForm[] Map(Session[] sessions)
		{
			return Map<Session[], SessionForm[]>(sessions);
		}
	}
}