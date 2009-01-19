using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Routes
{
	internal class IncomingRouteTester {}
	[TestFixture]
	public class RoutesTester
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			new RouteConfigurator().RegisterRoutes();
		}

		[Test]
		public void Speaker_actions_should_map_to_the_controller_action_without_the_conferenceKey()
		{
			"~/asdfd/speaker".Route().ShouldMapTo<SpeakerController>().WithAction<SpeakerController>(c => c.Index(null));
		}

		[Test]
		public void Confernce_actions_should_map_to_the_controller_action_without_the_conferenceKey()
		{
			"~/conference/new".ShouldMapTo<ConferenceController>(c => c.New());
		}

		[Test]
		public void Home_controller_routes_should_map_to_the_index()
		{
			"~/home".ShouldMapTo<HomeController>(c => c.Index());
		}

		[Test]
		public void Login_controller_routes_should_map_correctly()
		{
			"~/login".ShouldMapTo<LoginController>(c => c.Index());
			"~/login/login".Route().ShouldMapTo<LoginController>().WithAction<LoginController>(c => c.Login(null));
		}

		[Test]
		public void Unknown_root_names_should_map_to_the_conference_index_and_pass_the_conference_key()
		{
			"~/austinCodeCamp2008".Route().ShouldMapTo<ConferenceController>().WithAction<ConferenceController>(c => c.Index(null));
			//"austinCodeCamp2008"
		}


		//routes.MapRoute("login", "login/{action}", new { controller = "login", action = "index" });
		//routes.MapRoute("admin", "admin/{action}", new { controller = "admin", action = "edit" });
		//routes.MapRoute("home", "home/{action}", new { controller = "home", action = "index" });


		//RequestFor("~/austinCodeCamp2008").ShouldMatchController("conference").AndAction("index")
		//    .WithRouteValue("conferenceKey", "austinCodeCamp2008");

		//RequestFor("~/login").ShouldMatchController("login").AndAction("index");
		//RequestFor("~/conference/new").ShouldMatchController("conference").AndAction("new");
		//RequestFor("~/conference/current").ShouldMatchController("conference").AndAction("current");
		//RequestFor("~/admin").ShouldMatchController("admin").AndAction("index");
		//RequestFor("~/houstonTechFest/sessions/add").ShouldMatchController("sessions").AndAction("add")
		//    .WithRouteValue("conferenceKey", "houstonTechFest");

		//RequestFor("~/dallasCodeCamp/edit").ShouldMatchController("conference").AndAction("Edit").WithRouteValue(
		//    "conferenceKey", "dallasCodeCamp");

		//RequestFor("~/dallasCodeCamp/PleaseRegister").ShouldMatchController("conference").AndAction("PleaseRegister").WithRouteValue(
		//    "conferenceKey", "dallasCodeCamp");

		//RequestFor("~/myconf/speaker/jeffreypalermo").ShouldMatchController("speaker").AndAction("details")
		//    .WithRouteValue("id", "jeffreypalermo")
		//    .WithRouteValue("conferenceKey", "myconf");

		//RequestFor("~/myconf/schedule/Edit/41c0f584-69a3-4d4b-b1ea-9b1f017bf387/7db897d7-8f0a-47ee-abf4-9b1f017bf388").
		//    ShouldMatchController("schedule").AndAction("edit")
		//    .WithRouteValue("conferenceKey", "myconf")
		//    .WithRouteValue("trackId", "41c0f584-69a3-4d4b-b1ea-9b1f017bf387")
		//    .WithRouteValue("timeslotId", "7db897d7-8f0a-47ee-abf4-9b1f017bf388");
	}
}