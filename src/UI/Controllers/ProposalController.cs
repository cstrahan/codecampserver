using System.Web.Mvc;
using CodeCampServer.Core.Domain.Messages;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class ProposalController : SaveController<Proposal, IProposalMessage>
	{
		public ProposalController(IProposalRepository repository, IProposalMapper mapper)
			: base(repository, mapper) {}

		public ViewResult New(Conference conference)
		{
			var model = new ProposalForm {ConferenceKey = conference.Key};
			return View("Edit", model);
		}

		[ValidateModel(typeof (ProposalForm))]
		public ActionResult Save(ProposalForm form)
		{
			return ProcessSave(form, () => RedirectToAction<ProposalController>(c => c.Confirmation()));
		}

		private string Confirmation()
		{
			return "done";
		}
	}
}