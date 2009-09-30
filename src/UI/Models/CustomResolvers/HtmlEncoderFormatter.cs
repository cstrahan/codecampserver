using System.Web;
using AutoMapper;
using CodeCampServer.Core.Common;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class HtmlEncoderFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			return HttpUtility.HtmlEncode(PrimitiveExtensions.ToNullSafeString(context.SourceValue));
		}
	}
}