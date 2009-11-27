using System.Web.Mvc;
using CodeCampServer.UI;
using CodeCampServer.UI.Helpers.ActionResults;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public static class ActionResultSecurityExtension
	{
		public static ActionResult ShouldBeNotAuthorized(this ActionResult result)
		{
			result.AssertViewRendered().ForView(ViewPages.NotAuthorized);
			return result;
		}

		public static ViewResult ModelShouldBe<Type>(this ViewResult result)
		{
			result.ViewData.Model.ShouldBeInstanceOfType(typeof (Type));
			return result;
		}
		public static ViewResult AutoMappedModelShouldBe<Type>(this ViewResult result)
		{
			((AutoMappedViewResult)result).ViewModelType.ShouldEqual(typeof(Type));
			return result;
		}
	}
}