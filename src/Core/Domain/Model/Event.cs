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
	}
}