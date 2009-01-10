using System;
using CodeCampServer.Core.Common;
using CodeCampServer.Infrastructure.AutoMap;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class StandardTimeFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is DateTime))
				return context.SourceValue.ToNullSafeString();

			return ((DateTime) context.SourceValue).ToString("HH:mm");
		}
	}
}