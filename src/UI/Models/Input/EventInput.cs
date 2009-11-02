using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCampServer.UI.Models.Input
{
	public abstract class EventInput
	{
		[Required]
		public abstract DateTime StartDate { get; set; }

		[Required]
		public abstract DateTime EndDate { get; set; }

		[Required]
		public abstract string TimeZone { get; set; }

		public string GetDate()
		{
			try
			{
				string start = DateTime.Parse(StartDate.ToString()).ToString("h:mm");
				string end = DateTime.Parse(EndDate.ToString()).ToString("h:mm tt");
				string date = DateTime.Parse(StartDate.ToString()).ToShortDateString();

				return string.Format("{0} {1} - {2} {3}", date, start, end, TimeZone);
			}
			catch
			{
				;
			}
			return "";
		}
	}
}