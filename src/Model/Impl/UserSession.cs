using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class UserSession : IUserSession
	{
		private IAuthenticationService _authenticationService;
		private IAttendeeRepository _attendeeRepository;


		public UserSession(IAuthenticationService authenticationService, IAttendeeRepository attendeeRepository)
		{
			_authenticationService = authenticationService;
			_attendeeRepository = attendeeRepository;
		}

		public Attendee GetCurrentUser()
		{
			string username = _authenticationService.GetActiveUserName();
			Attendee currentUser = _attendeeRepository.GetAttendeeByEmail(username);
			return currentUser;
		}
	}
}