
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IKeyedRepository<T> : IRepository<T> where T : PersistentObject
	{
		T GetByKey(string key);
	}
}