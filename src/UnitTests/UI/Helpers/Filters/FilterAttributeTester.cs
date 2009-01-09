using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Helpers.Filters
{
	public abstract class FilterAttributeTester
	{
		protected static ActionExecutingContext GetActionExecutingContext(ControllerBase controller)
		{
			ControllerContext controllerContext = GetControllerContext(controller);
			controller.ControllerContext = controllerContext;
			var actionExecutingContext = new ActionExecutingContext(
				controllerContext, new Dictionary<string, object>());
			return actionExecutingContext;
		}

		protected static AuthorizationContext GetAuthorizationContext(ControllerBase controller)
		{
			ControllerContext controllerContext = GetControllerContext(controller);
			controller.ControllerContext = controllerContext;
			return new AuthorizationContext(controllerContext);
		}

		private static ControllerContext GetControllerContext(ControllerBase controller)
		{
			var mockResponse = MockRepository.GenerateStub<HttpResponseBase>();
			var mockHttpContext = MockRepository.GenerateStub<HttpContextBase>();
			mockHttpContext.Stub(c => c.Response).Return(mockResponse).Repeat.Any();
			mockHttpContext.Stub(c => c.Timestamp).Return(new DateTime(2001, 1, 1)).Repeat.Any();
			return new ControllerContext(mockHttpContext, new RouteData(), controller);
		}

		protected static ActionExecutedContext GetActionExecutedContext(ControllerBase controller)
		{
			ControllerContext controllerContext = GetControllerContext(controller);
			controller.ControllerContext = controllerContext;
			var actionExecutedContext = new ActionExecutedContext(controllerContext, false, null);
			return actionExecutedContext;
		}
	}

	public class TestController : Controller
	{
	}

	public class TestSubController : SubController
	{
	}
}