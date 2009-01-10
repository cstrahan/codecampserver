using CodeCampServer.Infrastructure.AutoMap;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class NullValueResolver : IValueResolver
	{
		public object Resolve(object model)
		{
			return null;
		}
	}
}