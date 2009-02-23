using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.Core.Services
{
	[TestFixture]
	public class ProposalCoordinatorTester : TestBase
	{
		[Test]
		public void Should_return_only_valid_state_commands_for_proposal()
		{
			var proposal = new Proposal();
			var user = new User();

			var factory = S<IStateCommandFactory>();
			var command1 = S<IStateCommand>();
			command1.Stub(x => x.IsValid(proposal, user)).Return(true);
			var command2 = S<IStateCommand>();
			command2.Stub(x => x.IsValid(proposal, user)).Return(false);

			factory.Stub(x => x.GetAllStateCommands()).Return(new[] {command1, command2});
			var session = S<IUserSession>();
			session.Stub(x => x.GetCurrentUser()).Return(user);

			var coordinator = new ProposalCoordinator(session, factory);
			IStateCommand[] commands = coordinator.GetValidCommands(proposal);

			commands.Length.ShouldEqual(1);
			commands[0].ShouldEqual(command1);
		}

		[Test]
		public void Should_execute_command_matching_command_name()
		{
			var proposal = new Proposal();
			var user = new User();

			var factory = S<IStateCommandFactory>();
			var command1 = S<IStateCommand>();
			command1.Stub(x => x.IsValid(proposal, user)).Return(true);
			var command2 = S<IStateCommand>();
			command2.Stub(x => x.IsValid(proposal, user)).Return(true);
			command2.Stub(x => x.Matches("foo")).Return(true);

			factory.Stub(x => x.GetAllStateCommands()).Return(new[] { command1, command2 });
			var session = S<IUserSession>();
			session.Stub(x => x.GetCurrentUser()).Return(user);

			var coordinator = new ProposalCoordinator(session, factory);
			coordinator.ExecuteCommand(proposal, "foo");

			command2.AssertWasCalled(x=>x.Execute(proposal, user));
		}
	}
}