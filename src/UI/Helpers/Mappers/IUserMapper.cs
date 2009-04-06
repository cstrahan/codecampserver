using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IUserMapper : IMapper<User, UserForm>
	{
        UserForm[] Map(User[] model);
        User[] Map(UserForm[] message);
	}
}