using System;
using AutoMapper;
using CodeCampServer.Core.Common;


namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.CustomResolvers
{
	public class StandardDateTimeFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is DateTime))
				return context.SourceValue.ToNullSafeString();

			return Format.DateAndTime((DateTime) context.SourceValue);
		}
	}
}