using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
	public class UserSessionStub : IUserSession
	{
		private readonly Attendee _attendee;
        private readonly Speaker _speaker;

        public UserSessionStub(Attendee attendee, Speaker speaker)
		{
			_attendee = attendee;
            _speaker = speaker;
		}

		public Attendee GetCurrentUser()
		{
			return _attendee;
		}

	    public Speaker GetLoggedInSpeaker()
	    {
	        return _speaker;
	    }
	}
}