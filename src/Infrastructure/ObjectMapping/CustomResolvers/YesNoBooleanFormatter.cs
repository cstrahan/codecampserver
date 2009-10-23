using AutoMapper;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Infrastructure.ObjectMapping.CustomResolvers
{
	public class YesNoBooleanFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is bool))
				return context.SourceValue.ToNullSafeString();

			return ((bool) context.SourceValue) ? "Yes" : "No";
		}
	}
}