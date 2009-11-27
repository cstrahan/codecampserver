using System;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup;
using CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup;
using CodeCampServer.Infrastructure.ObjectMapping.TypeConverters;
using CodeCampServer.Infrastructure.UI.Mappers;
using CodeCampServer.UI.Models.Input;
using CodeCampServer.UI.Models.Messages;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
{
	public class UserGroupMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<UserGroup, UserGroupInput>();
			Mapper.CreateMap<string, UserGroup>().ConvertUsing<GuidToUserGroupTypeConverter>();
			Mapper.CreateMap<Guid, UserGroup>().ConvertUsing<GuidToUserGroupTypeConverter>();
			Mapper.CreateMap<UserGroupInput, UserGroup>().ConvertUsing<UserGroupInputToUserGroupTypeConverter>();

			Mapper.CreateMap<UserGroupInput, UpdateUserGroupCommandMessage>()
				.ConvertUsing(input => new UpdateUserGroupCommandMessage
				                       	{
				                       		UserGroup = Mapper.Map<UserGroupInput, UserGroup>(input)
				                       	});
			Mapper.CreateMap<DeleteUserGroupInput, DeleteUserGroupCommandMessage>();

			Mapper.CreateMap<SponsorInput, UpdateSponsorCommandMessage>()
				.ConvertUsing(input =>

				              new UpdateSponsorCommandMessage()
				              	{
				              		Sponsor = Mapper.Map<SponsorInput, Sponsor>(input),
				              		UserGroup = Mapper.Map<Guid,UserGroup>(input.UserGroupId)
				              			}
				              	);
			Mapper.CreateMap<SponsorInput,Sponsor>().ConvertUsing<SponsorInputToSponsorTypeConverter>();

			Mapper.CreateMap<Sponsor, SponsorInput>()
				.ForMember(x => x.UserGroupId, o => o.Ignore());

		}
	}
}