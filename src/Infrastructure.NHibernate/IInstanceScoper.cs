using System;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public interface IInstanceScoper<T>
	{
		T GetScopedInstance(string key, Func<T> builder);
		void ClearScopedInstance(string key);
	}
}