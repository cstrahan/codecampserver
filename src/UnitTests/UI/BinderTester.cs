using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
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
			var context = new ModelBindingContext {ModelName = key, ValueProvider = CreateValueProvider(key, value)};
			return context;
		}

		protected static IValueProvider CreateValueProvider(string key, string value)
		{
			var values = new Dictionary<string, string> {{key, value}};
			return new DictionaryValueProvider<string>(values, CultureInfo.InvariantCulture);
		}
	}
}