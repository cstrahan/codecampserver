using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
	public class AttendeeListing
	{
		private Attendee _attendee;

		public AttendeeListing(Attendee attendee)
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