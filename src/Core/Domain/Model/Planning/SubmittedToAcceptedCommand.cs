using System;

namespace CodeCampServer.Core.Domain.Model.Planning
{
	public class SubmittedToAcceptedCommand : StateCommandBase
	{
		public override void Execute(Proposal proposal, User currentUser)
		{
			proposal.Status = ProposalStatus.Accepted;
		}

		public override string TransitionVerbPresentTense
		{
			get { return "Accept"; }
		}

		public override ProposalStatus GetBeginStatus()
		{
			return ProposalStatus.Submitted;
		}

		protected override bool IsUserAuthorized(Proposal proposal, User currentUser)
		{
			return currentUser.IsAdmin();
		}
	}
}