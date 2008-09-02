using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using CodeCampServer.Website.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website
{
	[TestFixture]
	public class RoutesTester
	{
	    private void AssertRoute(string virtualPath, string expectedController, string expectedAction)
	    {
	        AssertRoute(virtualPath, expectedController, expectedAction, new Dictionary<string, string>());
	    }

	    private void AssertRoute(string virtualPath, string expectedController, string expectedAction, IDictionary<string,string> expectedTokens)
        {
            var routeData = getMatchingRouteData(virtualPath);

            Assert.That(routeData.GetRequiredString("controller"), Is.EqualTo(expectedController));
            Assert.That(routeData.GetRequiredString("action"), Is.EqualTo(expectedAction));
            foreach (var pair in expectedTokens)
            {
                Assert.That(routeData.GetRequiredString(pair.Key), Is.EqualTo(pair.Value));
            }
        }	

	    [Test]
		public void TestSiteRoutes()
		{
	        AssertRoute("~/austinCodeCamp2008", "conference", "index",
	            new Dictionary<string, string> {{"conferenceKey", "austinCodeCamp2008"}});

	        AssertRoute("~/login", "login", "index");
	        AssertRoute("~/conference/new", "conference", "new");
	        AssertRoute("~/conference/current", "conference", "current");
	        AssertRoute("~/admin", "admin", "index");
	        AssertRoute("~/houstonTechFest/sessions/add", "sessions", "add",
                new Dictionary<string,string> {{"conferenceKey", "houstonTechFest"}});


            AssertRoute("~/myconf/speaker/jeffreypalermo", "speaker", "show", 
                new Dictionary<string, string>
                    {
                        {"id", "jeffreypalermo"},
                        {"conferenceKey", "myconf"}
                    });
		}

		private static RouteData getMatchingRouteData(string appRelativeUrl)
		{
			RouteTable.Routes.Clear();
			var configurator = new RouteConfigurator();
			configurator.RegisterRoutes();

			RouteData routeData;
			var mocks = new MockRepository();		   
			var httpContext = mocks.DynamicMock<HttpContextBase>();
			var request = mocks.DynamicMock<HttpRequestBase>();

			using (mocks.Record())
			{
				SetupResult.For(httpContext.Request).Return(request);
				mocks.Replay(httpContext);
				SetupResult.For(httpContext.Request.AppRelativeCurrentExecutionFilePath)
					.Return(appRelativeUrl);
				SetupResult.For(httpContext.Request.PathInfo)
					.Return(string.Empty);
			}

			using (mocks.Playback())
			{
				routeData = RouteTable.Routes.GetRouteData(httpContext);
			}

			return routeData;
		}
	}
}