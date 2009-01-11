namespace CodeCampServer.Core.Services
{
	public interface IMapper<TSource, TTarget>
	{
		TTarget Map(TSource sourceObject);
	}
}