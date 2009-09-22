
using CodeCampServer.Core.Services.Bases;

namespace CodeCampServer.Core.Domain.Model.Planning.StateCommands
{
	public class DraftToSubmittedCommand : StateCommandBase
	{
		private readonly ISystemClock _clock;

		public DraftToSubmittedCommand(ISystemClock clock)
		{
			_clock = clock;
		}

		public override void Execute(Proposal proposal, User currentUser)
		{
			proposal.Status = ProposalStatus.Submitted;
			proposal.SubmissionDate = _clock.GetCurrentDateTime();
		}

		public override string TransitionVerbPresentTense
		{
			get { return "Submit"; }
		}

		protected override ProposalStatus GetBeginStatus()
		{
			return ProposalStatus.Draft;
		}

		protected override bool IsUserAuthorized(Proposal proposal, User currentUser)
		{
			bool currentUserIsSubmitter = currentUser.Equals(proposal.Submitter);
			return currentUserIsSubmitter;
		}
	}
}