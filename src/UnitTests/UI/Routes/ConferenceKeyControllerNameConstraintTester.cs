using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
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
			Type[] types = typeof (HomeController).Assembly.GetTypes();
			IEnumerable<Type> controllers = types.Where(e => IsAController(e));
			foreach (Type controller in controllers)
			{
				Debug.WriteLine(controller.Name);
				constraint.Match("conferenceKey", controller.Name.Replace("Controller","")).ShouldBeFalse();
			}
		}

		private bool IsAController(Type e)
		{
			return e.GetInterface("IController")!=null;
		}
	}
}