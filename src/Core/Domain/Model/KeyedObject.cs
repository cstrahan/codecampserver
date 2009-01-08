using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
	public class KeyedObject : PersistentObject, IHasNaturalKey
	{
		public virtual string Key { get; set; }

		public virtual string GetNaturalKey()
		{
			return "Key";
		}
	}
}