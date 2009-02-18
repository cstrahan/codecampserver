using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public class ScheduleControllerTester : TestBase
	{
		[Test]
		public void Should_map_schedule_and_display()
		{
			var conference = new Conference();
			var scheduleForms = new ScheduleForm[0];

			var mapper = S<IScheduleMapper>();
			mapper.Stub(x => x.Map(conference)).Return(scheduleForms);

			var controller = new ScheduleController(mapper);
			ViewResult result = controller.Index(conference);
			result.ViewName.ShouldEqual("");
			result.ViewData.Model.ShouldEqual(scheduleForms);
		}
	}
}