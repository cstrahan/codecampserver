using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Services.Updaters
{
	public interface IModelUpdater<TModel, TMessage> where TModel : PersistentObject
	{
		UpdateResult<TModel, TMessage> UpdateFromMessage(TMessage message);
	}
}