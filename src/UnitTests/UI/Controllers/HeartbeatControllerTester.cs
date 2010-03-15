using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Display;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class HeartbeatControllerTester : ControllerTester
	{
		[Test]
		public void should_save_new_heartbeat()
		{
			var form = new HeartbeatInput {Message = "Howdy"};
			var controller = new HeartbeatController(null, null);

			var result = (CommandResult) controller.Edit(form);

			result.Success.AssertActionRedirect().ToAction<HeartbeatController>(a => a.Index());
		}

		[Test]
		public void should_list_on_index()
		{
			var top = new Heartbeat[3];

			var repository = S<IHeartbeatRepository>();
			repository.Stub(x => x.GetTop()).Return(top);

			var controller = new HeartbeatController(repository, null);

			var result = controller.Index();

			result.ViewData.Model.ShouldBeTheSameAs(top);
		}

		[Test]
		public void should_edit()
		{
			var controller = new HeartbeatController(null, null);

			var result = controller.Edit();

			var display = (HeartbeatInput) result.ViewData.Model;
			display.Message.ShouldBeNull();
		}

		[Test]
		public void should_check_the_heartbeat()
		{
			var timeout = 5;
			var message = "something";
			
			var checker = S<IHeartbeatChecker>();
			checker.Stub(x => x.CheckHeartbeat(timeout)).Return(message);

			var controller = new HeartbeatController(null, checker);
			var result = controller.Check(5);
			var checkResult = (HeartbeatCheckDisplay)result.ViewData.Model;
			checkResult.Message.ShouldEqual(message);
		}
	}
}