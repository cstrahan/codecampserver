using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IMapper<TModel, TForm> where TModel : PersistentObject, new()
	{
		TForm Map(TModel model);
		TModel Map(TForm form);
	}
}