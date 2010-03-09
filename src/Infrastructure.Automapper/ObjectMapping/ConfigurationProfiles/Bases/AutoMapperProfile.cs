using System;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.Automapper.ObjectMapping.CustomResolvers;
using CodeCampServer.UI.Models.Display;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles.Bases
{
	public class AutoMapperProfile : Profile
	{
		private const string _profileName = "CodeCampServer";

		protected override string ProfileName
		{
			get { return _profileName; }
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

			Mapper.CreateMap<Conference, ConferenceInput>()
				.ForMember(x => x.StartDate, o => o.AddFormatter<StandardDateTimeFormatter>())
				.ForMember(x => x.EndDate, o => o.AddFormatter<StandardDateTimeFormatter>());

			Mapper.CreateMap<Event, EventList>();
		}
	}
}