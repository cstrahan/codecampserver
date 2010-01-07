using System;

namespace CodeCampServer.Core.Domain.Bases
{
    [Serializable]
    public class ChangeAuditInfo : ValueObject<ChangeAuditInfo>
    {
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}