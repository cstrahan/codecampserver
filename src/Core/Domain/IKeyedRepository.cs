using CodeCampServer.Core.Domain.Model;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IKeyedRepository<T> : IRepository<T> where T : PersistentObject, IHasNaturalKey
	{
		T GetByKey(string key);
	}
}