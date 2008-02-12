using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class UserSession : IUserSession
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IAttendeeRepository _attendeeRepository;
        private readonly ISpeakerRepository _speakerRepository;

		public UserSession( IAuthenticationService authenticationService, 
                            IAttendeeRepository attendeeRepository,
                            ISpeakerRepository speakerRepository)
		{
			_authenticationService = authenticationService;
			_attendeeRepository = attendeeRepository;
            _speakerRepository = speakerRepository;
		}

		public Attendee GetCurrentUser()
		{
			string username = _authenticationService.GetActiveUserName();
			Attendee currentUser = _attendeeRepository.GetAttendeeByEmail(username);
			return currentUser;
		}
    
        public Speaker GetLoggedInSpeaker()
        {
            Attendee user = GetCurrentUser();

            if (user != null)
                return _speakerRepository.GetSpeakerByEmail(user.Contact.Email);
            else
                return null;
        }

    }
}