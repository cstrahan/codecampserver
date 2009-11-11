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
			new RouteConfigurator().RegisterRoutes(() => { return; });
		}

		[Test]
		public void Home_controller_routes_should_map_to_the_index()
		{
			"~/home".ShouldMapTo<HomeController>(c => c.Index(null))
				.ShouldUseDomainNameRouteHandler();
		}

		[Test]
		public void Root_should_map_to_home()
		{
			"~/".ShouldMapTo<HomeController>(c => c.Index(null))
				.ShouldUseDomainNameRouteHandler();
		}

		//[Test]
		//public void Confernce_actions_should_map_to_the_controller_action_without_the_conferenceKey()
		//{
		//    "~/conference/new"
		//        .ShouldMapTo<ConferenceController>(c => c.New(null))
		//        .ShouldUseDomainNameRouteHandler();
            
		//}

		//[Test]
		//public void Login_controller_routes_should_map_correctly()
		//{
		//    "~/login"
		//        .ShouldMapTo<LoginController>(c => c.Index((string)null))
		//        .ShouldUseDomainNameRouteHandler();
            
		//    "~/login/index"
		//        .ShouldMapTo<LoginController>(c => c.Index((string)null))
		//        .ShouldUseDomainNameRouteHandler();
		//}


		//[Test]
		//public void Unknown_root_names_should_map_to_the_conference_index_and_pass_the_conference_key()
		//{
		//    "~/austinCodeCamp2008"
		//        .ShouldMapTo<ConferenceController>(c => c.Index(null))
		//        .ShouldUseDomainNameRouteHandler();
		//}

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