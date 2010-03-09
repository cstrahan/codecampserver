using AutoMapper;
using CodeCampServer.Core.Services.BusinessRule.Login;
using CodeCampServer.UI;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles.Bases
{
	public class UserMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<LoginInputProxy, LoginUserCommandMessage>();
		}
	}
}