using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IKeyedRepository<T> : IRepository<T>
		where T : PersistentObject
	{
		T GetByKey(string key);
	}
}