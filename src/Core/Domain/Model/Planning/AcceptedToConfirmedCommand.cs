namespace CodeCampServer.Core.Domain.Model.Planning
{
	public class AcceptedToConfirmedCommand : StateCommandBase
	{
		public override void Execute(Proposal proposal, User currentUser)
		{
			proposal.Status = ProposalStatus.Confirmed;
		}

		public override string TransitionVerbPresentTense
		{
			get { return "Confirm"; }
		}

		public override ProposalStatus GetBeginStatus()
		{
			return ProposalStatus.Accepted;
		}

		protected override bool IsUserAuthorized(Proposal proposal, User currentUser)
		{
			return currentUser.IsAdmin();
		}
	}
}