using System;
using CodeCampServer.Core.Domain.Model.Enumerations;

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
		public virtual DateTime? SubmissionDate { get; set; }
		public virtual DateTime? CreatedDate { get; set; }
		public virtual int Votes { get; set; }

		public Proposal()
		{
			Status = ProposalStatus.Draft;
		}
	}
}