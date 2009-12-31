using System;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.ObjectMapping.TypeConverters;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
{
	public class MeetingMapperProfile : Profile
	{
		public static Func<Type, object> CreateDependencyCallback = (type) => Activator.CreateInstance(type);

		public T CreateDependency<T>()
		{
			return (T)CreateDependencyCallback(typeof(T));
		}
		protected override void Configure()
		{
			Mapper.CreateMap<Guid, Meeting>().ConvertUsing<GuidIdToEntityConverter<Meeting>>();
			Mapper.CreateMap<Meeting, MeetingInput>();
			Mapper.CreateMap<Meeting, MeetingAnnouncementDisplay>()
				.ForMember(x => x.Heading, o => o.MapFrom(m => m.Name))
				.ForMember(x => x.MeetingInfo, o => o.MapFrom(m => m.Description))
				.ForMember(x => x.When, o => o.MapFrom(m => new DateTimeSpan(m.StartDate, m.EndDate, m.TimeZone)));

			Mapper.CreateMap<MeetingInput, Meeting>().ConstructUsing(
				input => CreateDependency<IMeetingRepository>().GetById(input.Id) ?? new Meeting())

				.ForMember(x => x.UserGroup, o => o.MapFrom(x => x.UserGroupId))
				.ForMember(x => x.Address, o => o.Ignore())
				.ForMember(x => x.City, o => o.Ignore())
				.ForMember(x => x.Region, o => o.Ignore())
				.ForMember(x => x.PostalCode, o => o.Ignore())
				.ForMember(x => x.ChangeAuditInfo, o => o.Ignore());
		}
	}
}