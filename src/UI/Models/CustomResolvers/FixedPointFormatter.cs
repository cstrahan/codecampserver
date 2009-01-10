using CodeCampServer.Core.Common;
using CodeCampServer.Infrastructure.AutoMap;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class FixedPointFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is decimal))
				return context.SourceValue.ToNullSafeString();

			return ((decimal) context.SourceValue).ToString("f");
		}
	}
}