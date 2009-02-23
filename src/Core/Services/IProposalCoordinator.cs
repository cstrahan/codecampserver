using CodeCampServer.Core.Domain.Model.Planning;

namespace CodeCampServer.Core.Services
{
	public interface IProposalCoordinator
	{
		IStateCommand[] GetValidCommands(Proposal proposal);
		void ExecuteCommand(Proposal proposal, string commandName);
	}
}