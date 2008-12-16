using CodeCampServer.UI.Models.AutoMap;

namespace CodeCampServer.UI.Models
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.InitializeFormatting(x =>
                                                {
//                                                        x.AddFormatter<HtmlEncoderFormatter>();
//                                                        x.AddFormatter<SpanWrappingFormatter>();
//                                                        x.ForSourceType<DateTime>().AddFormatter<StandardDateTimeFormatter>();
//                                                        x.ForSourceType<bool>().AddFormatter<YesNoBooleanFormatter>();
//                                                        x.ForSourceType<DateTime?>().AddFormatter<StandardDateTimeFormatter>();
//                                                        x.ForSourceType<TimeSpan>().AddFormatter<TimeSpanFormatter>();
//                                                        x.ForSourceType<decimal>().AddFormatter<MoneyFormatter>();
//                                                        x.ForSourceType<decimal?>().AddFormatter<MoneyFormatter>();
//                                                        x.ForSourceType<float>().AddFormatter<AxisValueFormatter>();
//                                                        x.ForSourceType<float?>().AddFormatter<AxisValueFormatter>();
////														x.ForSourceType<Name>().AddFormatter(ObjectFactory.GetInstance<FullNameFormatter>());
//                                                        x.ForSourceType<Guid>().SkipFormatter<SpanWrappingFormatter>();
                                                });
        }
    }
}