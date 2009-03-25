using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
	public abstract class KeyedObject : PersistentObject
	{
		public virtual string Key { get; set; }
	}
}