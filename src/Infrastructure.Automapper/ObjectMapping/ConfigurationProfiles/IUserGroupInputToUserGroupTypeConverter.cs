using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles
{
	public interface IUserGroupInputToUserGroupTypeConverter:ITypeConverter<UserGroupInput, UserGroup>
	{
		void UpdateUserGroupFromInput(UserGroup model, UserGroupInput input);
	}
}