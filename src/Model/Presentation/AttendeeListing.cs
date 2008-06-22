using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
	public class AttendeeListing
	{
		private readonly Person _attendee;

		public AttendeeListing(Person attendee)
		{
			_attendee = attendee;
		}

		public string Name
		{
			get { return _attendee.GetName(); }
		}

		public string Email
		{
			get { return _attendee.Contact.Email; }
		}
	}
}