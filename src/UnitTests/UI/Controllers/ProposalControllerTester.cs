using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public class ProposalControllerTester : SaveControllerTester
	{
		[Test]
		public void Should_create_new_proposal_form()
		{
			var coordinator = S<IProposalCoordinator>();
			coordinator.Stub(x => x.GetValidCommands(null)).IgnoreArguments().Return(new IStateCommand[0]);
			var controller = new ProposalController(S<IProposalRepository>(), S<IProposalMapper>(), coordinator);
			var conference = new Conference {Key = "foo"};
			ViewResult result = controller.New(conference);
			result.ViewName.ShouldEqual("Edit");
			result.ViewData.Model.ShouldBeInstanceOfType(typeof (ProposalForm));
			((ProposalForm) result.ViewData.Model).ConferenceKey.ShouldEqual("foo");
		}

		[Test]
		public void Should_save_and_then_redirect_to_confirmation()
		{
			var form = new ProposalForm();
			var mapper = S<IProposalMapper>();
			var proposal = new Proposal {Id = Guid.NewGuid()};
			mapper.Stub(x => x.Map(form)).Return(proposal);
			var repository = S<IProposalRepository>();

			var coordinator = S<IProposalCoordinator>();
			coordinator.Stub(x => x.GetValidCommands(null)).Return(new IStateCommand[0]).IgnoreArguments();
			var controller = new ProposalController(repository, mapper, coordinator);
			var result = (RedirectToRouteResult) controller.Save(form, null);

			repository.AssertWasCalled(x => x.Save(proposal));
			result.RedirectsTo<ProposalController>(x => x.Edit(null)).ShouldBeTrue();
			result.RouteValues["proposalId"].ShouldEqual(proposal.Id);
		}

		[Test]
		public void Confirmation_should_send_proposal_to_view()
		{
			var mapper = S<IProposalMapper>();
			var proposal = new Proposal();
			var form = new ProposalForm();
			mapper.Stub(x => x.Map<ProposalForm>(proposal)).Return(form);
			var controller = new ProposalController(S<IProposalRepository>(), mapper, S<IProposalCoordinator>());
			ViewResult confirmation = controller.Confirmation(proposal);

			confirmation.ViewName.ShouldEqual("");
			confirmation.ViewData.Model.ShouldEqual(form);
		}

		[Test]
		public void Edit_should_send_proposal_to_view()
		{
			var mapper = S<IProposalMapper>();
			var proposal = new Proposal();
			var form = new ProposalForm();
			mapper.Stub(x => x.Map<ProposalForm>(proposal)).Return(form);
			var coordinator = S<IProposalCoordinator>();
			coordinator.Stub(x => x.GetValidCommands(proposal)).Return(new IStateCommand[0]);

			var controller = new ProposalController(S<IProposalRepository>(), mapper, coordinator);
			ViewResult viewResult = controller.Edit(proposal);

			viewResult.ViewName.ShouldEqual("");
			viewResult.ViewData.Model.ShouldEqual(form);
		}

		[Test]
		public void List_should_get_all_proposals_and_list_them()
		{
			var proposal1 = new Proposal();
			var proposal2 = new Proposal();
			var conference = new Conference();
			var repository = S<IProposalRepository>();
			repository.Stub(x => x.GetByConference(conference)).Return(new[] {proposal1, proposal2});
			var mapper = S<IProposalMapper>();
			var form1 = new ProposalForm();
			var form2 = new ProposalForm();
			mapper.Stub(x => x.Map<ProposalForm>(proposal1)).Return(form1);
			mapper.Stub(x => x.Map<ProposalForm>(proposal2)).Return(form2);

			var controller = new ProposalController(repository, mapper, S<IProposalCoordinator>());
			ViewResult result = controller.List(conference);
			result.ViewName.ShouldEqual("");
			result.ViewData.Model.ShouldEqual(new[] {form1, form2});
		}

	    [Test]
	    public void vote_should_increment_the_proposal_session_count()
	    {
	        var proposalid = Guid.NewGuid();
	        var proposal = new Proposal() {Id = proposalid, Votes = 3};
	        var originalVoteCount = proposal.Votes;
	        var repo = S<IProposalRepository>();
            repo.Stub(x => x.GetById(proposalid)).Return(proposal);
	        repo.Stub(x => x.Save(proposal));
            var controller = new ProposalController(repo, S<IProposalMapper>(), S<IProposalCoordinator>());
	        controller.Vote(proposalid);
	        Assert.That(proposal.Votes, Is.EqualTo(originalVoteCount+1));
            repo.AssertWasCalled(x=>x.Save(proposal));
	    }
	}
}