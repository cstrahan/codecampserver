
namespace CodeCampServer.Core.Domain.Model.Enumerations
{
	public class ConferenceType : Enumeration
	{
		public static ConferenceType Conference = new ConferenceType(1, "Conference");
		public static ConferenceType UserGroupMeeting = new ConferenceType(2, "User Group Meeting");

		public ConferenceType()
		{
			
		}

		public ConferenceType(int value, string displayName) : base(value, displayName)
		{
		}
	}
}