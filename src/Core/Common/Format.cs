using System;

namespace CodeCampServer.Core.Common
{
	public class Format
	{
		public static string DateAndTime(DateTime dateTime)
		{
			return dateTime.ToString("MM/dd/yyyy HH:mm");
		}

		public static string DateAndTime(DateTime? dateTime)
		{
			return dateTime.HasValue ? dateTime.Value.ToString("MM/dd/yyyy HH:mm") : string.Empty;
		}

		public static string Date(DateTime date)
		{
			return date.ToString("MM/dd/yyyy");
		}

		public static string Date(DateTime? date)
		{
			return date == null
			       	? string.Empty
			       	: Date(date.Value);
		}
	}
}