namespace CodeCampServer.Core.Domain.Model
{
	public abstract class KeyedObject : AuditedPersistentObject, IKeyable
	{
		public virtual string Key { get; set; }
	}
}