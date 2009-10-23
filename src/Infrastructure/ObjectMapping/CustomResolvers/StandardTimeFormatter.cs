using System;
using AutoMapper;
using CodeCampServer.Core.Common;


namespace CodeCampServer.Infrastructure.ObjectMapping.CustomResolvers
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