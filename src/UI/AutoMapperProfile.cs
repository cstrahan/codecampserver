using System;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.CustomResolvers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI
{
	public class AutoMapperProfile : Profile
	{
		private const string CodeCampServer = "CodeCampServer";

		protected override string ProfileName
		{
			get { return CodeCampServer; }
		}

		protected override void Configure()
		{
			AddFormatter<HtmlEncoderFormatter>();
			AddFormatter<SpanWrappingFormatter>();
			ForSourceType<DateTime>().AddFormatter<StandardDateFormatter>();
			ForSourceType<DateTime?>().AddFormatter<StandardDateFormatter>();
			ForSourceType<bool>().AddFormatter<YesNoBooleanFormatter>();
			ForSourceType<bool?>().AddFormatter<YesNoBooleanFormatter>();
			ForSourceType<TimeSpan>().AddFormatter<TimeSpanFormatter>();
			ForSourceType<decimal>().AddFormatter<MoneyFormatter>();
			ForSourceType<decimal?>().AddFormatter<MoneyFormatter>();
			ForSourceType<Guid>().SkipFormatter<SpanWrappingFormatter>();

			CreateMaps();
		}

		private static void CreateMaps()
		{
			Mapper.CreateMap<User, UserInput>()
				.ForMember(u => u.Password, o => o.Ignore())
				.ForMember(f => f.ConfirmPassword, o => o.Ignore());

			Mapper.CreateMap<User, UserSelectorInput>();

			Mapper.CreateMap<Sponsor, SponsorInput>()
				.ForMember(x => x.ParentID, o => o.Ignore());

			Mapper.CreateMap<Conference, ConferenceInput>()
				.ForMember(x => x.StartDate, o => o.AddFormatter<StandardDateTimeFormatter>())
				.ForMember(x => x.EndDate, o => o.AddFormatter<StandardDateTimeFormatter>());

			Mapper.CreateMap<Event, EventList>();
		}
	}
}