using System;
using System.Collections.Generic;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using System.Linq;

namespace CodeCampServer.Core.Services.Impl
{
	public class ProposalCoordinator : IProposalCoordinator
	{
		private readonly IUserSession _userSession;
		private readonly IStateCommandFactory _factory;

		public ProposalCoordinator(IUserSession userSession, IStateCommandFactory factory)
		{
			_userSession = userSession;
			_factory = factory;
		}

		public IStateCommand[] GetValidCommands(Proposal proposal)
		{
			User currentUser = _userSession.GetCurrentUser();
			IEnumerable<IStateCommand> validCommands = _factory.GetAllStateCommands()
				.Where(command => command.IsValid(proposal, currentUser));
			return validCommands.ToArray();
		}

		public void ExecuteCommand(Proposal proposal, string commandName)
		{
			IStateCommand match = GetValidCommands(proposal).Single(command=>command.Matches(commandName));
			User currentUser = _userSession.GetCurrentUser();
			match.Execute(proposal, currentUser);
		}
	}
}