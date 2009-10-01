
namespace CodeCampServer.Core.Domain.Model
{
	public abstract class KeyedObject : PersistentObject, IKeyable	
	{
		public virtual string Key { get; set; }
	}
}