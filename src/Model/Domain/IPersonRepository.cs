using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model.Domain
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IPersonRepository
	{
		void Save(Person person);
	    Person FindByEmail(string email);
        int GetNumberOfUsers();
    }
}
