using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Domain
{
	public interface IKeyedRepository<T> : IRepository<T> where T : PersistentObject
	{
		T GetByKey(string key);
	}
}