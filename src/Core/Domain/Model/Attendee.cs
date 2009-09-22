
namespace CodeCampServer.Core.Domain.Model
{
	public class Attendee : PersistentObject
	{
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual AttendanceStatus Status { get; set; }
		public virtual string Webpage { get; set; }
	}
}