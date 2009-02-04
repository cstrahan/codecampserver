using System;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Routes
{
	[TestFixture]//,Ignore("There is a bug in the RC which has broken this functionality.")]
	public class UrlGenerationTester
	{

		[Test]
		public void Should_correctly_generate_session_url()
		{
			new RouteConfigurator().RegisterRoutes();
			var builder = new TestControllerBuilder();
			var context = new RequestContext(builder.HttpContext, new RouteData());
			context.HttpContext.Response.Expect(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(new Func<string, string>(s => s)).Repeat.Any();
			var urlhelper = new UrlHelper(context);
			string url = urlhelper.RouteUrl("session", new { sessionKey = "this-is-the-session", conferenceKey = "austincodecamp" });
			url.ShouldEqual("/austincodecamp/sessions/this-is-the-session");
		}

		[Test]
		public void Should_correctly_generate_speaker_url()
		{
			new RouteConfigurator().RegisterRoutes();
			var builder = new TestControllerBuilder();
			var context = new RequestContext(builder.HttpContext, builder.RouteData);
			context.HttpContext.Response.Expect(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(new Func<string, string>(s => s)).Repeat.Any();
			var urlhelper =
			  new UrlHelper(
				context);
			string url = urlhelper.RouteUrl("speaker", new { conferenceKey = "austinCodeCamp", speakerKey = "ScottGuthrie" });
			url.ShouldEqual("/austinCodeCamp/speakers/ScottGuthrie");
		}
	}
}