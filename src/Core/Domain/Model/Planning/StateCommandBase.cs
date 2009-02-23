namespace CodeCampServer.Core.Domain.Model.Planning
{
	public abstract class StateCommandBase : IStateCommand
	{
		protected StateCommandBase() {}

		public abstract void Execute(Proposal proposal, User currentUser);
		public abstract string TransitionVerbPresentTense { get; }
		public abstract ProposalStatus GetBeginStatus();
		protected abstract bool IsUserAuthorized(Proposal proposal, User currentUser);

		public bool IsValid(Proposal proposal, User currentUser)
		{
			bool beginStatusMatches = proposal.Status == GetBeginStatus();
			bool userIsAuthorized = IsUserAuthorized(proposal, currentUser);
			return beginStatusMatches && userIsAuthorized;
		}

		public bool Matches(string commandName)
		{
			return TransitionVerbPresentTense == commandName;
		}
	}
}