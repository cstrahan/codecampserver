using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ScheduleMapper : IScheduleMapper
	{
		private readonly ISessionRepository _sessionRepository;
		private readonly ITrackRepository _trackRepository;
		private readonly ITimeSlotRepository _timeSlotRepository;
		private readonly ISessionMapper _sessionMapper;
		private readonly ITrackMapper _trackMapper;
		private readonly ITimeSlotMapper _timeSlotMapper;

		public ScheduleMapper(ISessionRepository sessionRepository, ITrackRepository trackRepository,
		                      ITimeSlotRepository timeSlotRepository, ISessionMapper sessionMapper, ITrackMapper trackMapper,
		                      ITimeSlotMapper timeSlotMapper)
		{
			_sessionRepository = sessionRepository;
			_trackRepository = trackRepository;
			_timeSlotRepository = timeSlotRepository;
			_sessionMapper = sessionMapper;
			_trackMapper = trackMapper;
			_timeSlotMapper = timeSlotMapper;
		}

		public ScheduleForm Map(Conference conference)
		{
			TrackForm[] tracks = _trackMapper.Map(_trackRepository.GetAllForConference(conference));
			TimeSlotForm[] timeSlots = _timeSlotMapper.Map(_timeSlotRepository.GetAllForConference(conference));
			SessionForm[] sessions = _sessionMapper.Map(_sessionRepository.GetAllForConference(conference));

			var timeSlotAssignments = new List<TimeSlotAssignmentForm>();
			foreach (var timeSlot in timeSlots)
			{
				TimeSlotForm currentTimeSlot = timeSlot;
				SessionForm[] matchingSessions = sessions.Where(s => s.TimeSlot.Id == currentTimeSlot.Id).ToArray();
				timeSlotAssignments.Add(CreateTimeSlotAssignment(currentTimeSlot, tracks, matchingSessions));
			}

			var form = new ScheduleForm {Tracks = tracks, TimeSlotAssignments = timeSlotAssignments.ToArray()};
			return form;
		}

		private static TimeSlotAssignmentForm CreateTimeSlotAssignment(TimeSlotForm timeSlot, IEnumerable<TrackForm> tracks,
		                                                               IEnumerable<SessionForm> sessions)
		{
			var assignments = new List<TrackAssignmentForm>();
			foreach (var track in tracks)
			{
				TrackForm currentTrack = track;
				SessionForm[] matchingSessions = sessions.Where(s => s.Track.Id == currentTrack.Id).ToArray();
				assignments.Add(CreateTrackAssignment(currentTrack, matchingSessions));
			}

			var form = new TimeSlotAssignmentForm {TimeSlot = timeSlot, TrackAssignments = assignments.ToArray()};
			return form;
		}

		private static TrackAssignmentForm CreateTrackAssignment(TrackForm track, SessionForm[] sessions)
		{
			return new TrackAssignmentForm() {Track = track, Sessions = sessions};
		}
	}
}