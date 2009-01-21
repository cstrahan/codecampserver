using System;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
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

			AutoMapper.Mapper.CreateMap<User, UserForm>()
				.ForMember(u => u.Password, o => o.Ignore())
				.ForMember(f => f.ConfirmPassword, o => o.Ignore());

			AutoMapper.Mapper.CreateMap<Conference, ConferenceForm>()
				.ForMember(x => x.StartDate, o => o.AddFormatter<StandardDateFormatter>())
				.ForMember(x => x.EndDate, o => o.AddFormatter<StandardDateFormatter>());

			AutoMapper.Mapper.CreateMap<Track, TrackForm>();

			AutoMapper.Mapper.CreateMap<TimeSlot, TimeSlotForm>();
			AutoMapper.Mapper.CreateMap<Speaker, SpeakerForm>();
			AutoMapper.Mapper.CreateMap<Session, SessionForm>();

			AutoMapper.Mapper.CreateMap<Attendee, AttendeeForm>()
				.ForMember(a => a.AttendeeID, o => o.MapFrom(a => a.Id))
				.ForMember(a => a.ConferenceID, o => o.Ignore());
		}
	}
}
