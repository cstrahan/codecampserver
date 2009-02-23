using System;
using System.Collections.Generic;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Domain.Model.Planning.StateCommands;

namespace CodeCampServer.Core.Services.Impl
{
	public class StateCommandFactory : IStateCommandFactory
	{
		private List<IStateCommand> _stateCommands = new List<IStateCommand>();

		public StateCommandFactory(SaveDraftCommand saveDraftCommand, DraftToSubmittedCommand draftToSubmittedCommand,
			SubmittedToAcceptedCommand submittedToAcceptedCommand, AcceptedToConfirmedCommand acceptedToConfirmedCommand)
		{
			_stateCommands.Add(saveDraftCommand);
			_stateCommands.Add(draftToSubmittedCommand);
			_stateCommands.Add(submittedToAcceptedCommand);
			_stateCommands.Add(acceptedToConfirmedCommand);
		}
		public IStateCommand[] GetAllStateCommands()
		{
			return _stateCommands.ToArray();
		}
	}
}