using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model
{
	public interface IUserSession
	{
		Person GetLoggedInPerson();
		bool IsAdministrator { get; }
		void PushUserMessage(FlashMessage.MessageType messageType, string message);
		FlashMessage[] PopUserMessages();
	}
}