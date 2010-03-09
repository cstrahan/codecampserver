using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.Core.Services.BusinessRule.UpdateMeeting;
using CodeCampServer.UI.Models.Input;
using CodeCampServer.UI.Models.Messages;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles
{
	public class MeetingMessageMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<DeleteMeetingMessage, DeleteMeetingCommandMessage>();
			Mapper.CreateMap<MeetingInput, UpdateMeetingCommandMessage>()
				.ConvertUsing(input => new UpdateMeetingCommandMessage
				                       	{
				                       		Meeting = Mapper.Map<MeetingInput, Meeting>(input),
				                       	});
		}
	}
}