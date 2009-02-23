using System;
using Tarantino.Core.Commons.Services.Environment;

namespace CodeCampServer.Core.Domain.Model.Planning
{
	public class SaveDraftCommand : StateCommandBase
	{
		private ISystemClock _clock;

		public SaveDraftCommand(ISystemClock clock)
		{
			_clock = clock;
		}

		public override void Execute(Proposal proposal, User currentUser)
		{
			proposal.Status = ProposalStatus.Draft;
			proposal.SubmissionDate = null;
			proposal.Submitter = currentUser;
			proposal.CreatedDate = proposal.CreatedDate ?? _clock.GetCurrentDateTime();
		}

		public override string TransitionVerbPresentTense
		{
			get { return "Save"; }
		}

		public override ProposalStatus GetBeginStatus()
		{
			return ProposalStatus.Draft;
		}

		protected override bool IsUserAuthorized(Proposal proposal, User currentUser)
		{
			bool currentUserIsSubmitter = currentUser.Equals(proposal.Submitter);
			return currentUserIsSubmitter || proposal.Submitter == null;
		}
	}
}