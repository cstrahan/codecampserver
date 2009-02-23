using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IMapper<TModel, TMessage> where TModel : PersistentObject, new()
	{
		TMessage Map(TModel model);
		TModel Map(TMessage message);
	}
}