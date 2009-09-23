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
			Mapper.CreateMap<User, UserForm>()
				.ForMember(u => u.Password, o => o.Ignore())
				.ForMember(f => f.ConfirmPassword, o => o.Ignore());

			Mapper.CreateMap<User, UserSelector>();


			Mapper.CreateMap<Sponsor, SponsorForm>()
				.ForMember(x => x.ParentID, o => o.Ignore());

			Mapper.CreateMap<Conference, ConferenceForm>()
				.ForMember(x => x.StartDate, o => o.AddFormatter<StandardDateTimeFormatter>())
				.ForMember(x => x.EndDate, o => o.AddFormatter<StandardDateTimeFormatter>());

			Mapper.CreateMap<Meeting, MeetingForm>()
				.ForMember(x => x.StartDate, o => o.AddFormatter<StandardDateTimeFormatter>())
				.ForMember(x => x.EndDate, o => o.AddFormatter<StandardDateTimeFormatter>());

			Mapper.CreateMap<UserGroup, UserGroupForm>();
			//.ForMember(a => a.Users, o => o.  MapFrom(a => a.GetUsers() ));
		}
	}
}