namespace CodeCampServer.Core.Domain.Model
{
	public class Speaker : KeyedObject
	{
		public virtual Conference Conference { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Company { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string JobTitle { get; set; }
		public virtual string Bio { get; set; }
		public virtual string WebsiteUrl { get; set; }
	}
}