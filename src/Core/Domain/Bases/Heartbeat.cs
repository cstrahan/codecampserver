using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Bases
{
	public class Heartbeat : AuditedPersistentObjectOfGuid
	{
		public virtual string Message { get; set; }
	}
}