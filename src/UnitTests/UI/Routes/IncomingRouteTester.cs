using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;

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
			"~/login/login".Route().ShouldMapTo<LoginController>(c => c.Login(null));
		}

		[Test]
		public void Session_index_should_use_the_session_key()
		{
			"~/austinCodeCamp2008/sessions/di-ioc-mvc-soc".Route()
				.ShouldMapTo<SessionController>(c => c.Index(null))
				.Values["sessionKey"].ShouldEqual("di-ioc-mvc-soc");
		}

		[Test]
		public void Speaker_actions_should_map_to_the_controller_action_without_the_conferenceKey()
		{
			"~/asdfd/speaker".Route().ShouldMapTo<SpeakerController>(c => c.Index(null));
		}

		[Test]
		public void Speakers_index_should_use_the_session_key()
		{
			"~/austinCodeCamp2008/speakers/fredflinstone".Route()
				.ShouldMapTo<SpeakerController>(c => c.Index(null))
				.Values["speakerKey"].ShouldEqual("fredflinstone");
		}

		[Test]
		public void Unknown_root_names_should_map_to_the_conference_index_and_pass_the_conference_key()
		{
			"~/austinCodeCamp2008".Route().ShouldMapTo<ConferenceController>(c => c.Index(null));
		}

		[Test]
		public void Proposal_actions_should_map_to_ProposalController()
		{
			"~/confkey/proposal/new".ShouldMapTo<ProposalController>(c => c.New(null));
		}
	}
}