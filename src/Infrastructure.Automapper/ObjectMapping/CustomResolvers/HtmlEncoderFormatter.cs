using System.Web;
using AutoMapper;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.CustomResolvers
{
	public class HtmlEncoderFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			return HttpUtility.HtmlEncode(context.SourceValue.ToNullSafeString());
		}
	}
}