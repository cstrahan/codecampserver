using System;

namespace CodeCampServer.Core.Domain.Model.Planning.StateCommands
{
	public class SubmittedToDraftCommand : StateCommandBase
	{
		public override void Execute(Proposal proposal, User currentUser)
		{
			proposal.Status = ProposalStatus.Draft;
		}

		public override string TransitionVerbPresentTense
		{
			get { return "Reject"; }
		}

		protected override ProposalStatus GetBeginStatus()
		{
			return ProposalStatus.Submitted;
		}

		protected override bool IsUserAuthorized(Proposal proposal, User currentUser)
		{
			return currentUser.IsAdmin();
		}
	}
}