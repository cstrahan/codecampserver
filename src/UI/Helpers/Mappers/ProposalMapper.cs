using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Messages;
using CodeCampServer.Core.Domain.Model.Planning;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ProposalMapper : AutoFormMapper<Proposal, IProposalMessage>, IProposalMapper
	{
		public ProposalMapper(IProposalRepository repository) : base(repository) {}
		
		protected override Guid GetIdFromMessage(IProposalMessage message)
		{
			return message.Id;
		}

		protected override void MapToModel(IProposalMessage message, Proposal model)
		{
			throw new NotImplementedException();
		}
	}
}