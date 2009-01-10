namespace CodeCampServer.Infrastructure.AutoMap
{
	public interface IValueResolver
	{
		object Resolve(object model);
	}
}