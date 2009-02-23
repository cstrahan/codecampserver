using CodeCampServer.Core.Domain.Model.Enumerations;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model.Planning
{
	public class Proposal : PersistentObject
	{
		public virtual User Submitter { get; set; }
		public virtual Conference Conference { get; set; }
		public virtual Track Track { get; set; }
		public virtual SessionLevel Level { get; set; }		
		public virtual string Title { get; set; }
		public virtual string Abstract { get; set; }
		public virtual ProposalStatus Status { get; set; }
	}
}