using System;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup;
using CodeCampServer.Core.Services.BusinessRule.UpdateSponsor;
using CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup;
using CodeCampServer.UI.Models.Input;
using CodeCampServer.UI.Models.Messages;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles
{
	public class UserGroupMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<UserGroup, UserGroupInput>();
			Mapper.CreateMap<string, UserGroup>().ConvertUsing<GuidToUserGroupTypeConverter>();
			Mapper.CreateMap<Guid, UserGroup>().ConvertUsing<IdToPersistentObjectTypeConverter<Guid, UserGroup>>();
			Mapper.CreateMap<int, Sponsor>().ConvertUsing<IdToPersistentObjectTypeConverter<int, Sponsor>>();
			Mapper.CreateMap<UserGroupInput, UserGroup>().ConvertUsing<UserGroupInputToUserGroupTypeConverter>();

			Mapper.CreateMap<UserGroupInput, UpdateUserGroupCommandMessage>()
				.ConvertUsing(input => new UpdateUserGroupCommandMessage
				                       	{
				                       		UserGroup = Mapper.Map<UserGroupInput, UserGroup>(input)
				                       	});
			Mapper.CreateMap<DeleteUserGroupInput, DeleteUserGroupCommandMessage>();

			Mapper.CreateMap<SponsorInput, UpdateSponsorCommandMessage>();

			Mapper.CreateMap<Sponsor, SponsorInput>().ForMember(x=>x.UserGroup, expression => expression.Ignore());
		}
	}
}