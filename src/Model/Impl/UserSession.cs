using System;
using System.Security.Principal;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Model.Impl
{
	public class UserSession : IUserSession
	{
		private readonly IAuthenticator _authenticator;
		private readonly IPersonRepository _personRepository;

		public UserSession(IAuthenticator authenticator, IPersonRepository personRepository)
		{
			_authenticator = authenticator;
			_personRepository = personRepository;
		}

		public virtual Person GetLoggedInPerson()
		{
			IIdentity identity = _authenticator.GetActiveIdentity();
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