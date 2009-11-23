using AutoMapper;
using CodeCampServer.Core.Services.BusinessRule.Login;
using CodeCampServer.UI.Models.Input;
using LoginPortableArea.Models;

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