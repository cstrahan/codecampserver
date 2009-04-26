using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.Core.Domain.Model.Planning;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings.Planning
{
	[TestFixture]
	public class ProposalMappingsTester : DataTestBase
	{
		[Test]
		public void should_map_proposal()
		{
			var conference = new Conference();
			var track = new Track();
			var user = new User();
			var proposal = new Proposal
			               	{
			               		Conference = conference,
			               		Submitter = user,
			               		Track = track,
			               		Level = SessionLevel.L100,
			               		Title = "title",
			               		Abstract = "abstract",
			               		Status = ProposalStatus.Submitted,
			               		SubmissionDate = new DateTime(2001, 1, 3),
								CreatedDate = new DateTime(2002, 1, 3),
                                Votes = 3
			               	};

			PersistEntities(conference, user, track);
			AssertObjectCanBePersisted(proposal);
		}
	}
}