
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Core.Domain.Model.Planning
{
	public class ProposalStatus : Enumeration
	{
		public static ProposalStatus Draft = new ProposalStatus(1, "Draft (not submitted)");
		public static ProposalStatus Submitted = new ProposalStatus(2, "Submitted (in review)");
		public static ProposalStatus Accepted = new ProposalStatus(3, "Accepted");
		public static ProposalStatus Confirmed = new ProposalStatus(4, "Confirmed");

		public ProposalStatus() {}
		public ProposalStatus(int value, string displayName) : base(value, displayName) {}
	}
}