namespace CodeCampServer.Core.Domain.Model
{
	public abstract class KeyedObject : AuditedPersistentObjectOfGuid, IKeyable
	{
		public virtual string Key { get; set; }
	}
}