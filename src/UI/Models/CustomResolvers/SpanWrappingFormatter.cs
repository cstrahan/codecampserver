using CodeCampServer.Core.Common;
using CodeCampServer.Infrastructure.AutoMap;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class SpanWrappingFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			string camelCaseMemberName = context.MemberName.ToLowerCamelCase();
			return string.Format(@"<span class=""{0}"">{1}</span>", camelCaseMemberName, context.SourceValue);
		}
	}
}