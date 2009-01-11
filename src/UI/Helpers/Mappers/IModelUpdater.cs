using CodeCampServer.UI.Helpers.Mappers;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IModelUpdater<TModel, TMessage> where TModel : PersistentObject
	{
		UpdateResult<TModel, TMessage> UpdateFromMessage(TMessage message);
	}
}