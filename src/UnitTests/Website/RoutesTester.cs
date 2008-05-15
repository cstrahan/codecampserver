using System.Web;
using System.Web.Routing;
using CodeCampServer.Website;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website
{
    [TestFixture]
    public class RoutesTester
    {
        [Test]
        public void Conference()
        {
            RouteData routeData = getMatchingRouteData("~/austincodecamp2008");

            Assert.That(routeData.GetRequiredString("controller"), Is.EqualTo("conference"));
            Assert.That(routeData.GetRequiredString("action"), Is.EqualTo("details"));
            Assert.That(routeData.Values["conferenceKey"], Is.EqualTo("austincodecamp2008"));
        }

        [Test]
        public void Login()
        {
            RouteData routeData = getMatchingRouteData("~/login");

            Assert.That(routeData.GetRequiredString("controller"), Is.EqualTo("login"));
            Assert.That(routeData.GetRequiredString("action"), Is.EqualTo("index"));
        }

        [Test]
        public void NewConference()
        {
            RouteData routeData = getMatchingRouteData("~/conference/new");

            Assert.That(routeData.GetRequiredString("controller"), Is.EqualTo("conference"));
            Assert.That(routeData.GetRequiredString("action"), Is.EqualTo("new"));
        }

        [Test]
        public void Admin()
        {
            RouteData routeData = getMatchingRouteData("~/admin");

            Assert.That(routeData.GetRequiredString("controller"), Is.EqualTo("admin"));
            Assert.That(routeData.GetRequiredString("action"), Is.EqualTo("index"));
        }

        [Test]
        public void Speaker()
        {
            RouteData routeData = getMatchingRouteData("~/myconf/speaker/jeffreypalermo");

            Assert.That(routeData.GetRequiredString("controller"), Is.EqualTo("speaker"));
            Assert.That(routeData.GetRequiredString("action"), Is.EqualTo("view"));
            Assert.That(routeData.Values["conferenceKey"], Is.EqualTo("myconf"));
            Assert.That(routeData.Values["speakerId"], Is.EqualTo("jeffreypalermo"));
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