using System;

namespace CodeCampServer.UI.Models.Input
{
	public class DateTimeSpan
	{
		private DateTime? _begin;
		private DateTime? _end;
		private readonly string _timeZone;

		public DateTimeSpan(DateTime? begin, DateTime? end, string timeZone)
		{
			_begin = begin;
			_end = end;
			_timeZone = timeZone;
		}

		public override string ToString()
		{
			string start = _begin.GetValueOrDefault().ToString("h:mm");
			string end = _end.GetValueOrDefault().ToString("h:mm tt");
			string date = _begin.GetValueOrDefault().ToShortDateString();

			return string.Format("{0} {1} - {2} {3}", date, start, end, _timeZone);
		}
	}
}