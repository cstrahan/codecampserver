using AutoMapper;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services.BusinessRule.CreateHeartbeat;
using CodeCampServer.UI.Models.Display;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles.Bases
{
	public class HeartbeatMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<HeartbeatInput, CreateHeartbeatCommandMessage>();
			Mapper.CreateMap<Heartbeat, HeartbeatDisplay>();
		}
	}
}