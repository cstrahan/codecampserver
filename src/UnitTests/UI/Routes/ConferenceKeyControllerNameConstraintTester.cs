using System;
using System.Diagnostics;
using System.Linq;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Services;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Routes
{
	[TestFixture]
	public class ConferenceKeyControllerNameConstraintTester
	{
		[Test]
		public void Should_allow_any_non_controller_names_into_this_route()
		{
			var constraint = new ConferenceKeyCannotBeAControllerNameConstraint();
			constraint.Match("conferenceKey", "foobar").ShouldBeTrue();
		}

		[Test]
		public void Should_not_allow_any_controller_names_into_this_route()
		{
			var constraint = new ConferenceKeyCannotBeAControllerNameConstraint();
			var types = typeof (HomeController).Assembly.GetTypes();
			var controllers = types.Where(e => IsAController(e));
			foreach (var controller in controllers)
			{
				Debug.WriteLine(controller.Name);
				constraint.Match("conferenceKey", controller.Name.Replace("Controller", "")).ShouldBeFalse();
			}
		}

		private bool IsAController(Type e)
		{
			return e.GetInterface("IController") != null;
		}
	}
}