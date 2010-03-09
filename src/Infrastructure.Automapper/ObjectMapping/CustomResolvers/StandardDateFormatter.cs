using System;
using AutoMapper;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.CustomResolvers
{
	public class StandardDateFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			if (context.SourceValue == null)
				return null;

			if (!(context.SourceValue is DateTime))
				return context.SourceValue.ToNullSafeString();

			return Format.Date((DateTime) context.SourceValue);
		}
	}
}