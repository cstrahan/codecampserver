using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Core.Domain.Messages
{
	public interface IProposalMessage
	{
		string ConferenceKey { get; set; }
		Track Track { get; set; }
		SessionLevel Level { get; set; }
		string Title { get; set; }
		string Abstract { get; set; }
		Guid Id { get; set; }
        int Votes { get; set; }
	}
}