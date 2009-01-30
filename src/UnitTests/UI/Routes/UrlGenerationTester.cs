using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Routes
{
	[TestFixture,Ignore("There is a bug in the RC which has broken this functionality.")]
	public class UrlGenerationTester
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			new RouteConfigurator().RegisterRoutes();
		}

		[Test]
		public void Should_correctly_generate_session_url()
		{
			var builder = new TestControllerBuilder();
			var context = new RequestContext(builder.HttpContext, new RouteData());
			var urlhelper = new UrlHelper(context);
			string url = urlhelper.RouteUrl("session", new {sessionKey = "this-is-the-session"});
			url.ShouldEqual("/sessions/this-is-the-session");
		}

		[Test]
		public void Should_correctly_generate_speaker_url()
		{
			new RouteConfigurator().RegisterRoutes();
			var builder = new TestControllerBuilder();
			var context = new RequestContext(builder.HttpContext, builder.RouteData);

			var urlhelper =
				new UrlHelper(
					context);

			string url = urlhelper.RouteUrl("speaker", new {conferenceKey = "austinCodeCamp", speakerKey = "ScottGuthrie"});
			url.ShouldEqual("austinCodeCamp/speakers/ScottGuthrie");
		}
	}
}