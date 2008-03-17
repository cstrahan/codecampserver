using System;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable(Keys.DEFAULT)]
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