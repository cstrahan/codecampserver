using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests
{
	[Obsolete("Use new RhionMocks syntax instead and don't fake TempData")]
	public static class Extensions
	{
		public static HttpContextBase FakeHttpContext(this MockRepository mocks)
		{
			var context = mocks.PartialMock<HttpContextBase>();
			var request = mocks.PartialMock<HttpRequestBase>();
			var response = mocks.PartialMock<HttpResponseBase>();
			var session = new MockHttpSessionState();
			var server = mocks.PartialMock<HttpServerUtilityBase>();

			SetupResult.For(context.Request).Return(request);
			SetupResult.For(context.Response).Return(response);
			SetupResult.For(context.Session).Return(session);
			SetupResult.For(context.Server).Return(server);

			mocks.Replay(context);
			return context;
		}

		public static HttpContextBase FakeHttpContext(this MockRepository mocks, string url)
		{
			HttpContextBase context = FakeHttpContext(mocks);
			context.Request.SetupRequestUrl(url);
			mocks.Replay(context.Request);
			return context;
		}

		public static void SetFakeControllerContext(this MockRepository mocks, Controller controller)
		{
			HttpContextBase httpContext = mocks.FakeHttpContext();
			var context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
			controller.ControllerContext = context;
		}

		private static string GetUrlFileName(string url)
		{
			if (url.Contains("?"))
				return url.Substring(0, url.IndexOf("?"));
			else
				return url;
		}

		private static NameValueCollection GetQueryStringParameters(string url)
		{
			if (url.Contains("?"))
			{
				string[] parts = url.Split('?');
				return HttpUtility.ParseQueryString(parts[1]);
			}
			else
			{
				return null;
			}
		}

		public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
		{
			SetupResult.For(request.HttpMethod).Return(httpMethod);
		}

		public static void SetupRequestUrl(this HttpRequestBase request, string url)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			if (!url.StartsWith("~/"))
				throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

			SetupResult.For(request.QueryString).Return(GetQueryStringParameters(url));
			SetupResult.For(request.AppRelativeCurrentExecutionFilePath).Return(GetUrlFileName(url));
			SetupResult.For(request.PathInfo).Return(string.Empty);
		}
	}
}