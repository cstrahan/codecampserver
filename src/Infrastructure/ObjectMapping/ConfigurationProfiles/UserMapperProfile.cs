using AutoMapper;
using CodeCampServer.Core.Services.BusinessRule.Login;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
{
	public class UserMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<LoginInput, LoginUserCommandMessage>();
		}
	}
}