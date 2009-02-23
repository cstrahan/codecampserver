using CodeCampServer.Core.Domain.Model.Planning;

namespace CodeCampServer.UI.Models
{
	public class ProposalEditInfo
	{
		public IStateCommand[] Commands { get; set; }
		public bool ReadOnly { get; set; }
	}
}