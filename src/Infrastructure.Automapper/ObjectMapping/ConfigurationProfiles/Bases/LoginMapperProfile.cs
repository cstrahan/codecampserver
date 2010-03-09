using AutoMapper;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.BusinessRule.UpdateUser;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles.Bases
{
	public class LoginMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<User, UserInput>()
				.ForMember(u => u.Password, o => o.Ignore())
				.ForMember(f => f.ConfirmPassword, o => o.Ignore());

			Mapper.CreateMap<User, UserSelectorInput>();
			Mapper.CreateMap<User[], UserInput[]>(); //.ConvertUsing<UserSelectorInputTypeConverter>();

			Mapper.CreateMap<UserInput, UpdateUserCommandMessage>();
		}
	}
}