using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Domain.Model.Planning.StateCommands;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Domain.Model.Planning.StateCommands
{
	[TestFixture]
	public class AcceptedToConfirmedCommandTester : TestBase
	{
		[Test]
		public void Should_be_valid_if_user_is_an_admin_and_status_is_correct()
		{
			var command = new AcceptedToConfirmedCommand();
			var user = S<User>();
			user.Stub(x => x.IsAdmin()).Return(true);

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Accepted }, user);
			valid.ShouldBeTrue();
		}

		[Test]
		public void Should_not_be_valid_if_user_is_not_admin_and_status_is_correct()
		{
			var command = new AcceptedToConfirmedCommand();
			var user = S<User>();
			user.Stub(x => x.IsAdmin()).Return(false);

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Accepted }, user);
			valid.ShouldBeFalse();
		}

		[Test]
		public void Should_not_be_valid_if_user_is_admin_and_status_is_incorrect()
		{
			var command = new AcceptedToConfirmedCommand();
			var user = S<User>();
			user.Stub(x => x.IsAdmin()).Return(true);

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Confirmed }, user);
			valid.ShouldBeFalse();
		}

		[Test]
		public void Should_match_correct_command_name()
		{
			new AcceptedToConfirmedCommand().Matches("Confirm").ShouldBeTrue();
			new AcceptedToConfirmedCommand().Matches("foobar").ShouldBeFalse();
		}

		[Test]
		public void Should_change_status_on_execute()
		{
			var command = new AcceptedToConfirmedCommand();
			var user = new User();
			var proposal = new Proposal();
			command.Execute(proposal, user);

			proposal.Status.ShouldEqual(ProposalStatus.Confirmed);
		}

	}
}