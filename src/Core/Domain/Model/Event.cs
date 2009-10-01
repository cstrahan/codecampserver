using System;

namespace CodeCampServer.Core.Domain.Model
{
	public abstract class Event : KeyedObject
	{
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual DateTime? StartDate { get; set; }
		public virtual DateTime? EndDate { get; set; }
		public virtual string LocationName { get; set; }
		public virtual string LocationUrl { get; set; }
		public virtual string Address { get; set; }
		public virtual string City { get; set; }
		public virtual string Region { get; set; }
		public virtual string PostalCode { get; set; }
		public virtual UserGroup UserGroup { get; set; }
		public virtual string TimeZone { get; set; }

		public virtual string Date()
		{
			try
			{
				string start = ((DateTime) StartDate).ToString("h:mm");
				string end = ((DateTime) EndDate).ToString("h:mm tt");
				string date = ((DateTime) StartDate).ToShortDateString();

				return string.Format("{0} {1} - {2} {3}", date, start, end, TimeZone);
			}
			catch
			{
				;
			}
			return "";
		}

		public abstract string Title();
	}
}