using CodeCampServer.Core.Services.Updaters;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core
{
	public interface IModelUpdater<TModel, TMessage> where TModel : PersistentObject
	{
		UpdateResult<TModel, TMessage> UpdateFromMessage(TMessage message);
	}
}