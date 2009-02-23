using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Domain.Model.Planning.StateCommands;
using CodeCampServer.Core.Services.Impl;
using NUnit.Framework;
using NBehave.Spec.NUnit;
using Tarantino.Core.Commons.Services.Environment;

namespace CodeCampServer.UnitTests.Core.Services
{
	[TestFixture]
	public class StateCommandFactoryTester : TestBase
	{
		[Test]
		public void Should_return_all_state_commands_in_proper_order()
		{
			var factory = new StateCommandFactory(new SaveDraftCommand(S<ISystemClock>()), new DraftToSubmittedCommand(S<ISystemClock>()), 
			                                      new SubmittedToAcceptedCommand(), new AcceptedToConfirmedCommand());
			IStateCommand[] commands = factory.GetAllStateCommands();
			commands.Length.ShouldEqual(4);
			commands[0].ShouldBeInstanceOfType(typeof(SaveDraftCommand));
			commands[1].ShouldBeInstanceOfType(typeof(DraftToSubmittedCommand));
			commands[2].ShouldBeInstanceOfType(typeof(SubmittedToAcceptedCommand));
			commands[3].ShouldBeInstanceOfType(typeof(AcceptedToConfirmedCommand));
		}
	}
}