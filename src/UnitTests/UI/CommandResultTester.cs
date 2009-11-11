using System;
using System.Web.Mvc;
using CodeCampServer.UI.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI
{
	public class CommandResultTester:TestBase
	{
		[Test]
		public void Result_should_wire_the_success_and_failure_results()
		{
			var result = new CommandResult<Message,Entity>(new Message(), entity => new ViewResult(){ViewName = "foo"},message=> new ViewResult() {ViewName = "bar"});
			result.Success.AssertViewRendered().ForView("foo");
			result.Failure.AssertViewRendered().ForView("bar");
		}
		
	}

	public class Entity {}

	public class Message {}
}