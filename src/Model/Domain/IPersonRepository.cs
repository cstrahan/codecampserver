using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Domain
{
	public interface IPersonRepository
	{
		void Save(Person person);
	    Person FindByEmail(string email);
        int GetNumberOfUsers();
    }
}
