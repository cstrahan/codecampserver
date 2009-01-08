using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
	public class KeyedObject : PersistentObject
	{
		public virtual string Key { get; set; }
	}
}