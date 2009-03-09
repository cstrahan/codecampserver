using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using System.Web.Routing;

namespace CodeCampServer.UnitTests.UI.Routes
{
	[TestFixture]
	public class RoutesTester
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			new RouteConfigurator().RegisterRoutes();
		}

		[Test]
		public void Confernce_actions_should_map_to_the_controller_action_without_the_conferenceKey()
		{
			"~/conference/new"
                .ShouldMapTo<ConferenceController>(c => c.New())
                .ShouldUseDomainNameRouteHandler();
            
		}

		[Test]
		public void Home_controller_routes_should_map_to_the_index()
		{
            "~/home".ShouldMapTo<HomeController>(c => c.Index())
                .ShouldUseDomainNameRouteHandler();
		}

		[Test]
		public void Login_controller_routes_should_map_correctly()
		{
            "~/login"
                .ShouldMapTo<LoginController>(c => c.Index())
                .ShouldUseDomainNameRouteHandler();
            
            "~/login/login"
                .ShouldMapTo<LoginController>(c => c.Login(null))
                .ShouldUseDomainNameRouteHandler();
		}

		[Test]
		public void Session_index_should_use_the_session_key()
		{
			"~/austinCodeCamp2008/sessions/di-ioc-mvc-soc"
				.ShouldMapTo<SessionController>(c => c.Index(null))
                .ShouldUseDomainNameRouteHandler()
				.Values["sessionKey"].ShouldEqual("di-ioc-mvc-soc");
		}

		[Test]
		public void Speaker_actions_should_map_to_the_controller_action_without_the_conferenceKey()
		{
		    "~/asdfd/speaker"
		        .ShouldMapTo<SpeakerController>(c => c.Index(null))
		        .ShouldUseDomainNameRouteHandler();
		}

		[Test]
		public void Speakers_index_should_use_the_session_key()
		{
			"~/austinCodeCamp2008/speakers/fredflinstone".Route()
				.ShouldMapTo<SpeakerController>(c => c.Index(null))
                .ShouldUseDomainNameRouteHandler()
				.Values["speakerKey"].ShouldEqual("fredflinstone");
		}

		[Test]
		public void Unknown_root_names_should_map_to_the_conference_index_and_pass_the_conference_key()
		{
		    "~/austinCodeCamp2008"
		        .ShouldMapTo<ConferenceController>(c => c.Index(null))
		        .ShouldUseDomainNameRouteHandler();
		}

		[Test]
		public void Proposal_actions_should_map_to_ProposalController()
		{
			"~/confkey/proposal/new"
                .ShouldMapTo<ProposalController>(c => c.New(null))
                .ShouldUseDomainNameRouteHandler();
		}
	}

    public static  class RouteTestExtension
    {
        public static RouteData ShouldUseDomainNameRouteHandler(this RouteData routeData)
        {
            routeData.RouteHandler.ShouldBeAssignableFrom(typeof (DomainNameRouteHandler));
            return routeData;
        }
    }
}