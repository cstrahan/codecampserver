using System;
using CodeCampServer.UI.Models.Input;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Models.Forms
{
	[TestFixture]
	public class DateTimeSpanTester
	{
		[Test]
		public void Should_get_formatted_date()
		{
			var span = new DateTimeSpan(new DateTime(2000, 1, 1, 8, 30, 0), 
				new DateTime(2000, 1, 1, 17, 0, 0), "cst");
			span.ToString().ShouldEqual("1/1/2000 8:30 - 5:00 PM cst");
		}
	}
}