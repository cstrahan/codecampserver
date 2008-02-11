using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class ConferenceService : IConferenceService
	{
		private readonly IConferenceRepository _conferenceRepository;
		private readonly IAttendeeRepository _attendeeRepository;
		private readonly ILoginService _loginService;
		private readonly ISpeakerRepository _speakerRepository;
		private readonly ISessionRepository _sessionRepository;
		private readonly IUserSession _userSession;

		public ConferenceService(IConferenceRepository conferenceRepository, IAttendeeRepository attendeeRepository,
		                         ILoginService loginService, ISpeakerRepository speakerRepository,
		                         ISessionRepository sessionRepository, IUserSession userSession)
		{
			_conferenceRepository = conferenceRepository;
			_attendeeRepository = attendeeRepository;
			_loginService = loginService;
			_speakerRepository = speakerRepository;
			_sessionRepository = sessionRepository;
			_userSession = userSession;
		}

		public Conference GetConference(string conferenceKey)
		{
			return _conferenceRepository.GetConferenceByKey(conferenceKey);
		}

		public IEnumerable<Conference> GetAllConferences()
		{
			return _conferenceRepository.GetAllConferences();
		}

		public Attendee[] GetAttendees(Conference conference, int page, int perPage)
		{
			return _attendeeRepository.GetAttendeesForConference(conference, page, perPage);
		}

		//TODO:  This method should be broken out to a AttendeeService, IAttendeeService
		public Attendee RegisterAttendee(string firstName, string lastName, string website, string comment,
		                                 Conference conference, string emailAddress, string cleartextPassword)
		{
			string passwordSalt = _loginService.CreateSalt();
			string encryptedPassword = _loginService.CreatePasswordHash(cleartextPassword, passwordSalt);

			Attendee attendee = new Attendee(firstName, lastName, website, comment, conference, emailAddress, encryptedPassword,
			                                 passwordSalt);
			_attendeeRepository.Save(attendee);
			return attendee;
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

		public Speaker GetLoggedInSpeaker()
		{
			Attendee user = _userSession.GetCurrentUser();
			
			if (user != null)
				return _speakerRepository.GetSpeakerByEmail(user.Contact.Email);
			else
				return null;
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

		public Session CreateSession(Speaker speaker, string title,
		                             string @abstract, OnlineResource[] onlineResources)
		{
			Session session = new Session(speaker, title, @abstract, onlineResources);
			_sessionRepository.Save(session);
			return session;
		}


		public IEnumerable<Session> GetProposedSessions(Conference conference)
		{
			return _sessionRepository.GetProposedSessions(conference);
		}
	}
}