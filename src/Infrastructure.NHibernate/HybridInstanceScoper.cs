using System;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public class HybridInstanceScoper<T> : IInstanceScoper<T>
	{
		private readonly ThreadStaticInstanceScoper<T> _threadStaticInstanceScoper;
		private readonly HttpContextInstanceScoper<T> _httpContextInstanceScoper;

		public HybridInstanceScoper()
		{
			_threadStaticInstanceScoper = new ThreadStaticInstanceScoper<T>();
			_httpContextInstanceScoper = new HttpContextInstanceScoper<T>();
		}

		public T GetScopedInstance(string key, Func<T> builder)
		{
			IInstanceScoper<T> scoper = GetScoperToUse();
			return scoper.GetScopedInstance(key, builder);
		}

		public void ClearScopedInstance(string key)
		{
			IInstanceScoper<T> scoper = GetScoperToUse();
			scoper.ClearScopedInstance(key);
		}

		private IInstanceScoper<T> GetScoperToUse()
		{
			if(_httpContextInstanceScoper.IsEnabled())
			{
				return _httpContextInstanceScoper;
			}

			return _threadStaticInstanceScoper;
		}
	}
}