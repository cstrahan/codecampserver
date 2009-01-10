using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace CodeCampServer.Core.Common
{
	public static class PrimitiveExtensions
	{
		public static bool ToBool(this bool? value)
		{
			if (value == null)
				return false;
			return value.Value;
		}

		public static string ToXHTMLLink(this string url)
		{
			return !string.IsNullOrEmpty(url) ? url.Replace("&", "&amp;") : string.Empty;
		}

		public static string ToStandardDateWithTime(this DateTime? value, string valueIfNull)
		{
			if (value == null)
				return String.Empty;

			return value.Value.ToString("MM/dd/yyyy HH:mm");
		}

		public static string ToStandardDateWithTime(this DateTime value)
		{
			return value.ToString("MM/dd/yyyy HH:mm");
		}

		public static string ToStandardDate(this DateTime? value, string valueIfNull)
		{
			if (value.HasValue) return value.Value.ToString("MM/dd/yyyy");
			return valueIfNull;
		}

		public static string ToFormattedString(this TimeSpan value)
		{
			var list = new List<string>(3);

			int days = value.Days;
			int hours = value.Hours;
			int minutes = value.Minutes;

			if (days > 1)
				list.Add(String.Format("{0} days", days));
			else if (days == 1)
				list.Add(String.Format("{0} day", days));

			if (hours > 1)
				list.Add(String.Format("{0} hours", hours));
			else if (hours == 1)
				list.Add(String.Format("{0} hour", hours));

			if (minutes > 1)
				list.Add(String.Format("{0} minutes", minutes));
			else if (minutes == 1)
				list.Add(String.Format("{0} minute", minutes));

			return String.Join(", ", list.ToArray());
		}

		public static string ToNullSafeString(this object value)
		{
			return value == null ? String.Empty : value.ToString();
		}

		public static string ToLowerCamelCase(this string value)
		{
			return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
		}

		public static string ToSeparatedWords(this string value)
		{
			return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
		}

		public static string WrapEachWith(this IEnumerable values, string before, string after, string separator)
		{
			var list = new List<string>();
			foreach (var value in values)
			{
				list.Add(string.Format("{0}{1}{2}", before, value, after));
			}
			return string.Join(separator, list.ToArray());
		}

		public static Dictionary<string, object> ToDictionary(this NameValueCollection nameValueCollection)
		{
			var dictionary = new Dictionary<string, object>();
			foreach (string key in nameValueCollection)
			{
				dictionary.Add(key, nameValueCollection[key]);
			}
			return dictionary;
		}

		public static DateTime? ToNullableDate(this string value)
		{
			DateTime result;
			return !DateTime.TryParse(value, out result) ? (DateTime?)null : result;
		}
	}
}