using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UnitTests.Website
{
	public class ControllerContextStub : ControllerContext
	{
		public ControllerContextStub(IController controller)
			: base(new RequestContext(new HttpContextStub(),
			                          new RouteData()), controller)
		{
		}

		public ControllerContextStub(IController controller,
		                             HttpContextBase contextBase)
			: base(new RequestContext(contextBase, new RouteData()),
			       controller)
		{
		}
	}
}