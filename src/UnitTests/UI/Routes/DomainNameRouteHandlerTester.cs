using System.Collections.Specialized;
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
			var handler = CreateHandler(routeData, "codecampserver.org:443");

			handler.ShouldBeAssignableFrom(typeof (MvcHandler));
			routeData.Values.ContainsKey("usergroupkey").ShouldBeTrue();
			routeData.Values["usergroupkey"].ShouldEqual("codecampserver_org");
		}

		[Test]
		public void The_handler_should_pass_the_domain_name_when_a_subdomain_exists_on_the_host_header()
		{
			var routeData = new RouteData();
			var handler = CreateHandler(routeData, "www.1codecampserver.org");

			handler.ShouldBeAssignableFrom(typeof (MvcHandler));
			routeData.Values.ContainsKey("usergroupkey").ShouldBeTrue();
			routeData.Values["usergroupkey"].ShouldEqual("1codecampserver_org");
		}

		[Test]
		public void The_handler_should_pass_the_domain_name_when_a_simple_host_name_exists_on_the_host_header()
		{
			var routeData = new RouteData();
			var handler = CreateHandler(routeData, "localhost:8080");

			handler.ShouldBeAssignableFrom(typeof (MvcHandler));
			routeData.Values.ContainsKey("usergroupkey").ShouldBeTrue();
			routeData.Values["usergroupkey"].ShouldEqual("localhost");
		}

		private IHttpHandler CreateHandler(RouteData routeData, string url)
		{
			var builder = new TestControllerBuilder();
			builder.RawUrl = url;
			var collection = new NameValueCollection();
			collection.Add("HTTP_HOST", url);
			builder.HttpContext.Request.Stub(@base => @base.ServerVariables).Return(collection);

			IRouteHandler routeHandler = new DomainNameRouteHandler();

			return routeHandler.GetHttpHandler(new RequestContext(builder.HttpContext, routeData));
		}
	}
}