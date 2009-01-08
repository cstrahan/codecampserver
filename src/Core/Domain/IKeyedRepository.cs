using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IKeyedRepository<T> : IRepository<T> where T : KeyedObject
	{
		T GetByKey(string key);
	}
}