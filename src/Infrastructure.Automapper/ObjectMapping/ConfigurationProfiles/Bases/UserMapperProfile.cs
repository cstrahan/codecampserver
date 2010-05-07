using AutoMapper;
using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles.Bases
{
	public class UserMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<LoginProxyInput, LoginProxyCommandMessage>();
		}
	}
}