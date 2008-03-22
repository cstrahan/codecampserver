using System;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Model.Impl
{
	public class UserSession : IUserSession
	{
		private readonly IAuthenticationService _authenticationService;
	    private readonly IPersonRepository _personRepository;	    

		public UserSession(IAuthenticationService authenticationService, IPersonRepository personRepository)
		{
			_authenticationService = authenticationService;
		    _personRepository = personRepository;
		}

        public Speaker GetLoggedInSpeaker()
        {
            throw new NotImplementedException();
        }

	    public Person GetLoggedInPerson()
        {
            string username = _authenticationService.GetActiveUserName();
            Person p = _personRepository.FindByEmail(username);

            return p;
	    }
	}
}