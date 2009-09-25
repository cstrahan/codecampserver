using System;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public abstract class EventForm
	{
		[BetterValidateDateTime("Start Date")]
		public abstract DateTime StartDate { get; set; }

		[BetterValidateDateTime("End Date")]
		public abstract DateTime EndDate { get; set; }

		[BetterValidateNonEmpty("Time Zone")]
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