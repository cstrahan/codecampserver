using System.Security.Principal;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Security
{
	public interface IAuthenticator
	{
		void SignIn(Person person);
		IIdentity GetActiveIdentity();
		void SignOut();
		bool VerifyAccount(Person person, string password);
	}
}