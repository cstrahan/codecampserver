using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI
{
	public abstract class BinderTester : TestBase
	{
		protected static ControllerContext GetControllerContext(string key, object value)
		{
			var queryCollection = new NameValueCollection
			                      	{
			                      		{key, value.ToString()}
			                      	};

			var mockRequest = MockRepository.GenerateStub<HttpRequestBase>();
			mockRequest.Stub(r => r.Form).Return(new NameValueCollection()).Repeat.Any();
			mockRequest.Stub(r => r.QueryString).Return(queryCollection).Repeat.Any();

			var mockHttpContext = MockRepository.GenerateStub<HttpContextBase>();
			mockHttpContext.Stub(c => c.Request).Return(mockRequest).Repeat.Any();

			var routeData = new RouteData();
			return new ControllerContext(mockHttpContext, routeData,
			                             MockRepository.GenerateStub<ControllerBase>());
		}

		protected static ModelBindingContext CreateBindingContext(string key, string value)
		{
			ValueProviders.Providers.Clear();
			ValueProviders.Providers.Add(CreateValueProvider(key, value));
			var context = new ModelBindingContext();
			context.ModelName = key;
			return context;
		}

		private static IValueProvider CreateValueProvider(string key, string value)
		{
			var provider = new SimpleValueProvider();
			provider.Add(key, value);
			return provider;
		}
	}
}