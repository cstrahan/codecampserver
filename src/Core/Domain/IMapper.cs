using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IMapper<TModel, TMessage> where TModel : PersistentObject, new()
	{
		TMessage Map(TModel model);
		TMessage2 Map<TMessage2>(TModel model);
		TModel Map(TMessage message);
	}
}