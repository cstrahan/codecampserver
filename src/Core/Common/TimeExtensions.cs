using System;

namespace CodeCampServer.Core.Common
{
	public static class TimeExtensions
	{
		/// <summary>
		/// Gets a DateTime representing midnight on the current date
		/// </summary>
		/// <param name="current">The current date</param>
		public static DateTime Midnight(this DateTime current)
		{
			var midnight = new DateTime(current.Year, current.Month, current.Day);
			return midnight;
		}

		/// <summary>
		/// Gets a DateTime representing noon on the current date
		/// </summary>
		/// <param name="current">The current date</param>
		public static DateTime Noon(this DateTime current)
		{
			var noon = new DateTime(current.Year, current.Month, current.Day, 12, 0, 0);
			return noon;
		}

		/// <summary>
		/// Sets the time of the current date with minute precision
		/// </summary>
		/// <param name="current">The current date</param>
		/// <param name="hour">The hour</param>
		/// <param name="minute">The minute</param>
		public static DateTime SetTime(this DateTime current, int hour, int minute)
		{
			return SetTime(current, hour, minute, 0, 0);
		}

		/// <summary>
		/// Sets the time of the current date with second precision
		/// </summary>
		/// <param name="current">The current date</param>
		/// <param name="hour">The hour</param>
		/// <param name="minute">The minute</param>
		/// <param name="second">The second</param>
		/// <returns></returns>
		public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
		{
			return SetTime(current, hour, minute, second, 0);
		}

		/// <summary>
		/// Sets the time of the current date with millisecond precision
		/// </summary>
		/// <param name="current">The current date</param>
		/// <param name="hour">The hour</param>
		/// <param name="minute">The minute</param>
		/// <param name="second">The second</param>
		/// <param name="millisecond">The millisecond</param>
		/// <returns></returns>
		public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
		{
			var atTime = new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
			return atTime;
		}
	}
}