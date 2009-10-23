using AutoMapper;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Infrastructure.ObjectMapping.CustomResolvers
{
	public class MoneyFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is decimal))
				return context.SourceValue.ToNullSafeString();

			return ((decimal) context.SourceValue).ToString("c");
		}
	}
}