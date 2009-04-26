using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Messages;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	[Obsolete("The invalid scenario here is really bad.  Redesign!")]
	[RequiresConferenceFilter]
	public class ProposalController : SaveController<Proposal, IProposalMessage>
	{
		private readonly IProposalRepository _repository;
		private readonly IProposalMapper _mapper;
		private readonly IProposalCoordinator _proposalCoordinator;

		public ProposalController(IProposalRepository repository, IProposalMapper mapper,
		                          IProposalCoordinator proposalCoordinator)
			: base(repository, mapper)
		{
			_repository = repository;
			_proposalCoordinator = proposalCoordinator;
			_mapper = mapper;
		}

		public ViewResult New(Conference conference)
		{
			AddEditInfoToViewData(new Proposal());
			var model = new ProposalForm {ConferenceKey = conference.Key};
			return View("Edit", model);
		}

		[ValidateModel(typeof (ProposalForm))]
		public ActionResult Save(ProposalForm form, string command)
		{
			AddEditInfoToViewData(form);
			return ProcessSave(form, PostSaveAction, delegate(Proposal obj)
			                                         	{
			                                         		PreSaveAction(obj, command);
			                                         	});
		}

		private void PreSaveAction(Proposal model, string command)
		{
			_proposalCoordinator.ExecuteCommand(model, command);
		}

		private RedirectToRouteResult PostSaveAction(Proposal proposal)
		{
			return RedirectToAction<ProposalController>(c => c.Edit(null), new {proposalId = proposal.Id});
		}

		public ViewResult Confirmation(Proposal proposal)
		{
			var message = _mapper.Map<ProposalForm>(proposal);
			return View(message);
		}

		public ViewResult Edit(Proposal proposal)
		{
			AddEditInfoToViewData(proposal);
			var message = _mapper.Map<ProposalForm>(proposal);
			return View(message);
		}

		private void AddEditInfoToViewData(ProposalForm form)
		{
			if(form.IsTransient())
			{
				AddEditInfoToViewData(new Proposal());
			}
			else
			{
				Proposal proposal = _repository.GetById(form.Id);
				AddEditInfoToViewData(proposal);
			}
		}

		private void AddEditInfoToViewData(Proposal proposal)
		{
			IStateCommand[] commands = GetValidCommands(proposal);
			ProposalEditInfo info = CreateEditInfo(commands);
			ViewData.Add(info);
		}

		private ProposalEditInfo CreateEditInfo(IStateCommand[] commands)
		{
			return new ProposalEditInfo {Commands = commands, ReadOnly = (commands.Length == 0)};
		}

		private IStateCommand[] GetValidCommands(Proposal proposal)
		{
			return _proposalCoordinator.GetValidCommands(proposal);
		}

		public ViewResult List(Conference conference)
		{
			Proposal[] proposals = _repository.GetByConference(conference);
			var forms = new List<ProposalForm>(proposals.Select(proposal => _mapper.Map<ProposalForm>(proposal)));
			return View(forms.ToArray());
		}

	    public ActionResult Vote(Guid id)
	    {
	        var proposal = _repository.GetById(id);
	        proposal.Votes++;
            _repository.Save(proposal);
	        return View();
	    }
	}
}