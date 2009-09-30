using System;
using CodeCampServer.UI.Models.Input;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Models.Forms
{
	[TestFixture]
	public class EventInputTester
	{
		[Test]
		public void Should_get_formatted_date()
		{
			var meeting = new MeetingInput();
			meeting.StartDate = DateTime.Parse( "1/1/2000 8:30 am");
			meeting.EndDate = DateTime.Parse("1/1/2000 5:00 pm");
			meeting.TimeZone = "cst";
			meeting.GetDate().ShouldEqual("1/1/2000 8:30 - 5:00 PM cst");
		}
	}
}