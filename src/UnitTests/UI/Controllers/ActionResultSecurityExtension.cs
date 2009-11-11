using System.Web.Mvc;
using CodeCampServer.UI;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public static class ActionResultSecurityExtension
	{
		public static void ShouldBeNotAuthorized(this ActionResult result)
		{
			result.AssertViewRendered().ForView(ViewPages.NotAuthorized);
		}

		public static void ModelShouldBe<Type>(this ViewResult result)
		{
			result.ViewData.Model.ShouldBeInstanceOfType(typeof (Type));
		}
	}
}