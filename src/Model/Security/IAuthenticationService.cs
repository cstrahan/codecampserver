using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Security
{
	public interface IAuthenticationService
	{
		void SignIn(Person person);
        string GetActiveUserName();
	    void SignOut();
	}
}
