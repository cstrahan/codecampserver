using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
	public class UserSessionStub : IUserSession
	{
	    private readonly Person _person;

	    public UserSessionStub(Person person)
		{
		    _person = person;
		}

	    public Person GetLoggedInPerson()
	    {
	        return _person;
	    }
	}
}