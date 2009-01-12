using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI.Models.CustomResolvers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI
{
	public class CodeCampServerProfile : Profile
	{
		public const string VIEW_MODEL = "CodeCampServer";

		protected override string ProfileName
		{
			get { return VIEW_MODEL; }
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

			AutoMapper.CreateMap<User, UserForm>()
				.ForMember(u => u.Password, o => o.Ignore())
				.ForMember(f => f.ConfirmPassword, o => o.Ignore());

			AutoMapper.CreateMap<Conference, ConferenceForm>()
				.ForMember(x => x.StartDate, o => o.AddFormatter<StandardDateFormatter>())
				.ForMember(x => x.EndDate, o => o.AddFormatter<StandardDateFormatter>());

			AutoMapper.CreateMap<Track, TrackForm>();

			AutoMapper.CreateMap<TimeSlot, TimeSlotForm>();
			AutoMapper.CreateMap<Speaker, SpeakerForm>();
		}
	}
}