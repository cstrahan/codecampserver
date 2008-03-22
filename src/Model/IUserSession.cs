using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model
{
	public interface IUserSession
	{
	    Person GetLoggedInPerson();	    
	}
}