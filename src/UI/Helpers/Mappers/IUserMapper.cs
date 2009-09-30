using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IUserMapper : IMapper<User, UserInput>
	{
        UserInput[] Map(User[] model);
        User[] Map(UserInput[] message);
	}
}