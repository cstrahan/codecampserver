using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain
{
	public interface IKeyedRepository<T> : IRepository<T> where T : PersistentObject
	{
		T GetByKey(string key);
	}
}