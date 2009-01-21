using System;
using System.Collections.Generic;
using AutoMapper;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class TimeSpanFormatter : IValueFormatter
	{
		public string FormatValue(ResolutionContext context)
		{
			var timespan = (TimeSpan) context.SourceValue;
			var list = new List<string>(3);

			int days = timespan.Days;
			int hours = timespan.Hours;
			int minutes = timespan.Minutes;

			if (days > 1)
				list.Add(string.Format("{0} days", days));
			else if (days == 1)
				list.Add(string.Format("{0} day", days));

			if (hours > 1)
				list.Add(string.Format("{0} hours", hours));
			else if (hours == 1)
				list.Add(string.Format("{0} hour", hours));

			if (minutes > 1)
				list.Add(string.Format("{0} minutes", minutes));
			else if (minutes == 1)
				list.Add(string.Format("{0} minute", minutes));

			return string.Join(", ", list.ToArray());
		}
	}
}