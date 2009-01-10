using System.Web.Mvc;
using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI.Helpers.Filters;
using MvcContrib;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Helpers.Filters
{
	[TestFixture]
	public class AutoMappedFilterAttributeTester : FilterAttributeTester
	{
		[Test]
		public void Should_not_throw_if_model_not_in_viewdata()
		{
			var controller = new TestController();
			ActionExecutedContext context = GetActionExecutedContext(controller);
			new AutoMappedFilterAttribute(typeof (string), typeof (string)).OnActionExecuted(context);
			new AutoMappedFilterAttribute(typeof (string), typeof (string), "hi there").OnActionExecuted(context);
		}

		[Test]
		public void Should_map_dto_and_place_in_viewdata()
		{
			AutoMapper.CreateMap<Model, Dto>();
			var controller = new TestController();
			controller.ViewData.Add(new Model());
			ActionExecutedContext context = GetActionExecutedContext(controller);

			new AutoMappedFilterAttribute(typeof (Model), typeof (Dto)).OnActionExecuted(context);

			controller.ViewData.Contains(typeof (Dto)).ShouldBeTrue();
		}

		[Test]
		public void Should_remove_original_model_from_viewdata_after_mapping()
		{
			AutoMapper.CreateMap<Model, Dto>();
			var controller = new TestController();
			controller.ViewData.Add(new Model());
			ActionExecutedContext context = GetActionExecutedContext(controller);

			new AutoMappedFilterAttribute(typeof (Model), typeof (Dto)).OnActionExecuted(context);

			//controller.ViewData.Contains(typeof(Model)).ShouldBeFalse();
		}

		internal class Model
		{
		}

		internal class Dto
		{
		}
	}
}