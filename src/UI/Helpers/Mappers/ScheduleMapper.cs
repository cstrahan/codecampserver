using System;
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


	    public ScheduleForm[] Map(Conference conference)
		{
			TimeSlot[] allTimeSlots = _timeSlotRepository.GetAllForConference(conference);
			DateTime startDate = allTimeSlots.Min(slot => slot.StartTime).GetValueOrDefault();
			DateTime endDate = allTimeSlots.Max(slot => slot.EndTime).GetValueOrDefault();
			TimeSpan daySpan = endDate.Subtract(startDate);
			var totalDays = (int) Math.Ceiling(daySpan.TotalDays);

			var formsList = new List<ScheduleForm>();
			for (int i = 0; i < totalDays; i++)
			{
				DateTime dayStart = startDate.AddDays(i);
				DateTime dayEnd = startDate.AddDays(i + 1);
				TimeSlot[] timeSlotsInDay = allTimeSlots.Where(s => s.StartTime >= dayStart
				                                                    && s.EndTime <= dayEnd).ToArray();
                if(timeSlotsInDay.Length>0)
                {
                    var dayNumber = formsList.Count() + 1;                
    				ScheduleForm form = CreateScheduleForSpecificDay(conference, dayStart, timeSlotsInDay, dayNumber);
                    formsList.Add(form);
                }
			}

			return formsList.ToArray();
		}

		public virtual ScheduleForm CreateScheduleForSpecificDay(Conference conference, DateTime dayStart, TimeSlot[] timeSlotsInDay, int dayNumber)
		{
			ScheduleForm form = CreateScheduleForDay(timeSlotsInDay, _trackRepository.GetAllForConference(conference),
			                                         _sessionRepository.GetAllForConference(conference));

			form.Day = dayNumber;
			form.Date = dayStart.ToString("dddd MM/dd");
			return form;
		}

		private ScheduleForm CreateScheduleForDay(TimeSlot[] timeSlots, Track[] tracks, Session[] sessions)
		{
			TimeSlotForm[] timeSlotsForms = _timeSlotMapper.Map(timeSlots);
			TrackForm[] trackForms = _trackMapper.Map(tracks);
			SessionForm[] sessionForms = _sessionMapper.Map(sessions);

			var timeSlotAssignments = new List<TimeSlotAssignmentForm>();
			foreach (TimeSlotForm timeSlot in timeSlotsForms)
			{
				TimeSlotForm currentTimeSlot = timeSlot;
				SessionForm[] matchingSessions = sessionForms.Where(s => s.TimeSlot.Id == currentTimeSlot.Id).ToArray();
				timeSlotAssignments.Add(CreateTimeSlotAssignment(currentTimeSlot, trackForms, matchingSessions));
			}

			var form = new ScheduleForm {Tracks = trackForms, TimeSlotAssignments = timeSlotAssignments.ToArray()};
			return form;
		}

		private static TimeSlotAssignmentForm CreateTimeSlotAssignment(TimeSlotForm timeSlot, IEnumerable<TrackForm> tracks,
		                                                               IEnumerable<SessionForm> sessions)
		{
			var assignments = new List<TrackAssignmentForm>();
			foreach (TrackForm track in tracks)
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
			return new TrackAssignmentForm {Track = track, Sessions = sessions};
		}
	}
}