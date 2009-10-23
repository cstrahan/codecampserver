using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.CreateMeeting;
using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
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