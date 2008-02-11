using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
	public class UserSessionStub : IUserSession
	{
		private Attendee _attendee;

		public UserSessionStub(Attendee attendee)
		{
			_attendee = attendee;
		}

		public Attendee GetCurrentUser()
		{
			return _attendee;
		}
	}
}