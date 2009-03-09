using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Routes
{
    [TestFixture]
    public class DomainNameRouteHandlerTester : TestBase
    {

        [Test]
        public void The_handler_should_pass_the_domain_name_as_route_data()
        {
            var routeData = new RouteData();
            IHttpHandler handler = CreateHandler(routeData, "http://codecampserver.org/foo/bar");

            handler.ShouldBeAssignableFrom(typeof (MvcHandler));
            routeData.Values.ContainsKey("domainname").ShouldBeTrue();
            routeData.Values["domainname"].ShouldEqual("codecampserver.org");
        }

        private IHttpHandler CreateHandler(RouteData routeData, string url) {
            var builder = new TestControllerBuilder();
            builder.RawUrl = url;
            builder.HttpContext.Request.Stub(@base => @base.Url).Return(new Uri(builder.RawUrl));

            IRouteHandler routeHandler = new DomainNameRouteHandler();

            return routeHandler.GetHttpHandler(new RequestContext(builder.HttpContext, routeData));
        }
    }
}