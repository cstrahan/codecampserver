using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Models.Forms
{
	[TestFixture]
	public class EventFormTester
	{
		[Test]
		public void Should_get_formatted_date()
		{
			var meeting = new MeetingForm();
			meeting.StartDate = "1/1/2000 8:30 am";
			meeting.EndDate = "1/1/2000 5:00 pm";
			meeting.TimeZone = "cst";
			meeting.GetDate().ShouldEqual("1/1/2000 8:30 - 5:00 PM cst");
		}
	}
}