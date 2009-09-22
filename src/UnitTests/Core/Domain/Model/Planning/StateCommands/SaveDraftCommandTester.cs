using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Domain.Model.Planning.StateCommands;
using CodeCampServer.Core.Services.Bases;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Domain.Model.Planning.StateCommands
{
	[TestFixture]
	public class SaveDraftCommandTester : TestBase
	{
		[Test]
		public void Should_be_valid_if_user_is_submitter_and_status_is_correct()
		{
			var command = new SaveDraftCommand(S<ISystemClock>());
			var user = new User();

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Draft, Submitter = user}, user);
			valid.ShouldBeTrue();
		}

		[Test]
		public void Should_be_valid_if_submitter_blank_and_status_is_correct()
		{
			var command = new SaveDraftCommand(S<ISystemClock>());
			var user = new User();

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Draft}, user);
			valid.ShouldBeTrue();
		}

		[Test]
		public void Should_not_be_valid_if_user_is_not_submitter_and_status_is_correct()
		{
			var command = new SaveDraftCommand(S<ISystemClock>());
			var user = new User();

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Draft, Submitter = new User()}, user);
			valid.ShouldBeFalse();
		}

		[Test]
		public void Should_not_be_valid_if_user_is_submitter_and_status_is_incorrect()
		{
			var command = new SaveDraftCommand(S<ISystemClock>());
			var user = new User();

			bool valid = command.IsValid(new Proposal { Status = ProposalStatus.Accepted, Submitter = user}, user);
			valid.ShouldBeFalse();
		}

		[Test]
		public void Should_match_correct_command_name()
		{
			new SaveDraftCommand(S<ISystemClock>()).Matches("Save").ShouldBeTrue();
			new SaveDraftCommand(S<ISystemClock>()).Matches("foobar").ShouldBeFalse();
		}

		[Test]
		public void Should_change_status_on_execute()
		{
			var clock = S<ISystemClock>();
			clock.Stub(x => x.GetCurrentDateTime()).Return(new DateTime(2000, 1, 1));

			var command = new SaveDraftCommand(clock);
			var user = new User();
			var proposal = new Proposal();
			command.Execute(proposal, user);

			proposal.Status.ShouldEqual(ProposalStatus.Draft);
			proposal.SubmissionDate.ShouldBeNull();
			proposal.Submitter.ShouldEqual(user);
			proposal.CreatedDate.ShouldEqual(new DateTime(2000, 1, 1));
		}
	}
}