using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Impl
{
	public class UserSession : IUserSession
	{
		private readonly IAuthenticator _authenticator;
		private readonly IPersonRepository _personRepository;
		private readonly IHttpContextProvider _httpContextProvider;

		public UserSession(IAuthenticator authenticator, IPersonRepository personRepository,
		                   IHttpContextProvider httpContextProvider)
		{
			_authenticator = authenticator;
			_httpContextProvider = httpContextProvider;
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

		private void PushUserMessage(FlashMessage message)
		{
			ensureFlashMessagesInitialized();
			Stack<FlashMessage> flash = getFlash();
			flash.Push(message);
		}

		public void PushUserMessage(FlashMessage.MessageType messageType, string message)
		{
			var flashMessage = new FlashMessage(messageType, message);
			PushUserMessage(flashMessage);
		}

		public FlashMessage[] PopUserMessages()
		{
			var messages = new List<FlashMessage>();
			FlashMessage message = PopMessage();
			while (message != null)
			{
				messages.Add(message);
				message = PopMessage();
			}

			return messages.ToArray();
		}

		private FlashMessage PopMessage()
		{
			var flash = getFlash();
			if (flash.Count == 0)
			{
				return null;
			}

			return flash.Pop();
		}

		private Stack<FlashMessage> getFlash()
		{
			ensureFlashMessagesInitialized();
			HttpSessionStateBase session = _httpContextProvider.GetHttpSession();
			return (Stack<FlashMessage>) session["flash"];
		}

		private void ensureFlashMessagesInitialized()
		{
			HttpSessionStateBase session = _httpContextProvider.GetHttpSession();
			var flash = session["flash"] as Stack<FlashMessage>;
			if (flash == null)
			{
				session["flash"] = new Stack<FlashMessage>();
			}
		}
	}
}