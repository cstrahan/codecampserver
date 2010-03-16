using System;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper.Ui;
using NUnit.Framework;


namespace CodeCampServerUiTests
{
	[TestFixture]
	public class MeetingEditViewTester : UiTestBase
	{
		[Test]
		public void Should_create_a_new_meeting()
		{
			_webBrowser.ScreenCaptureOnFailure(() =>
			   {
				   Form<LoginInputProxy>("/login/login/index")
					   .Input(m => m.Username, "admin")
					   .Input(m => m.Password, "password")
					   .Submit();

				   _webBrowser.VerifyPage<HomeController>(p => p.Index(null));

				   Form<MeetingInput>(GetUrl("meeting", "new"))
					  .Input(m => m.Name, "TX")
					  .Input(m => m.Topic, "my topic")
					  .Input(m => m.Summary, "this will be a normal meeting")
					  .Input(m => m.Description, "The description")
					  .Input(m => m.Key, Guid.NewGuid().ToString())
					  .Input(m => m.LocationName, "our location")
					  .Input(m => m.LocationUrl, "http://foolocation.com")
					  .Input(m => m.SpeakerBio, "this is a great speaker")
					  .Input(m => m.SpeakerName, "bart simpson")
					  .Input(m => m.SpeakerUrl, "http://thesimpsons.com")
					  .Input(m => m.TimeZone, "CST")
					  .Input(m => m.StartDate, "12/11/2010 12:00 pm")
					  .Input(m => m.EndDate, "12/11/2010 1:00 pm")
					  .Submit();
				   _webBrowser.VerifyPage<HomeController>(p => p.Index(null));
			   });
		}

		[Test]
		public void Should_require_fields()
		{
			_webBrowser.ScreenCaptureOnFailure(() =>
			   {
				   Form<LoginInputProxy>("/login/login/index")
					   .Input(m => m.Username, "admin")
					   .Input(m => m.Password, "password")
					   .Submit();
				   _webBrowser.VerifyPage<HomeController>(p => p.Index(null));

				   Form<MeetingInput>("/Meeting/New")
					   .Input(m => m.Name, "foo")
					   .Submit();
				   _webBrowser.VerifyPage<MeetingController>(p => p.Edit(null));

				   _webBrowser.ValidationSummaryExists();
				   _webBrowser.ValidationSummaryContainsMessageFor<MeetingInput>(
					   m => m.Topic);
				   _webBrowser.AssertValue<MeetingInput>(m => m.Name, "foo");
			   });
		}
	}
}