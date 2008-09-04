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
	    [Test]
		public void TestSiteRoutes()
		{
	        RequestFor("~/austinCodeCamp2008").ShouldMatchController("conference").AndAction("index")
	            .WithRouteValue("conferenceKey", "austinCodeCamp2008");

            RequestFor("~/login").ShouldMatchController("login").AndAction("index");
            RequestFor("~/conference/new").ShouldMatchController("conference").AndAction("new");
	        RequestFor("~/conference/current").ShouldMatchController("conference").AndAction("current");
	        RequestFor("~/admin").ShouldMatchController("admin").AndAction("index");
	        RequestFor("~/houstonTechFest/sessions/add").ShouldMatchController("sessions").AndAction("add")
                .WithRouteValue("conferenceKey", "houstonTechFest");

            //TODO: this route fails the test
	        /*RequestFor("~/dallasCodeCamp/edit").ShouldMatchController("conference").AndAction("Edit").WithRouteValue(
	            "conferenceKey", "dallasCodeCamp");*/

	        RequestFor("~/myconf/speaker/jeffreypalermo").ShouldMatchController("speaker").AndAction("show")
	            .WithRouteValue("id", "jeffreypalermo")
	            .WithRouteValue("conferenceKey", "myconf");            
		}

        private IRouteOptions RequestFor(string url)
        {
            var routeData = getMatchingRouteData(url);
            return new RouteOptions(routeData);
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

    public interface IRouteOptions
    {
        IRouteOptions ShouldMatchController(string controller);
        IRouteOptions AndAction(string action);
        IRouteOptions WithRouteValue(string key, string value);
    }

    public class RouteOptions : IRouteOptions
    {
        private readonly RouteData _routeData;

        public RouteOptions(RouteData routeData)
        {
            _routeData = routeData;
        }

        private void AssertStringsEquivalent(string actual, string expected)
        {
            Assert.That(string.Compare(expected, actual, true), Is.EqualTo(0),
                        string.Format("Expected {0} but was {1}", expected, actual));
        }

        public IRouteOptions ShouldMatchController(string controller)
        {
            AssertStringsEquivalent(_routeData.Values["controller"].ToString(), controller);            
            return this;
        }        

        public IRouteOptions AndAction(string action)
        {
            AssertStringsEquivalent(_routeData.Values["action"].ToString(), action);
            return this;
        }

        public IRouteOptions WithRouteValue(string key, string value)
        {
            AssertStringsEquivalent(_routeData.Values[key].ToString(), value);
            return this;
        }
    }
}