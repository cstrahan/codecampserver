using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Services.Unique
{
	public interface IEntityCounter
	{
		int CountByProperty<TModel>(IEntitySpecification<TModel> specification) where TModel : PersistentObject;
	}
}