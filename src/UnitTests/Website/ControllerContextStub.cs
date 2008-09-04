using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UnitTests.Website
{
	public class ControllerContextStub : ControllerContext
	{
		public ControllerContextStub(ControllerBase controller)
			: base(new HttpContextStub(), new RouteData(), controller)
		{
		}

		public ControllerContextStub(ControllerBase controller, HttpContextBase contextBase)
			: base(new RequestContext(contextBase, new RouteData()), controller)
		{
		}
	}
}