using CodeCampServer.Infrastructure.AutoMap;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public abstract class BaseResolver<T, TReturn> : IValueResolver
	{
		public object Resolve(object model)
		{
			return ResolveCore((T) model);
		}

		protected abstract TReturn ResolveCore(T model);
	}
}