using System;
using System.Security.Principal;
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

		public virtual Person GetLoggedInPerson()
		{
			IIdentity identity = _authenticationService.GetActiveIdentity();
			if (!identity.IsAuthenticated)
			{
				return null;
			}

			Person p = _personRepository.FindByEmail(identity.Name);

			return p;
		}

		public virtual bool IsAdministrator
		{
			get
			{
				Person person = GetLoggedInPerson();
				return person != null && person.IsAdministrator;
			}
		}

		public virtual void PushUserMessage(FlashMessage message)
		{
			throw new NotImplementedException();
		}

		public virtual FlashMessage PopUserMessage()
		{
			throw new NotImplementedException();
		}
	}
}