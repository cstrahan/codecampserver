using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	[Serializable]
	public class ChangeAuditInfo : ValueObject<ChangeAuditInfo>
	{
		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}