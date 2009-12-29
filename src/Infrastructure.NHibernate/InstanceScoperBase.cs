using System;
using System.Collections;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public abstract class InstanceScoperBase<T> : IInstanceScoper<T>
	{
		protected abstract IDictionary GetDictionary();

		public T GetScopedInstance(string key, Func<T> builder)
		{
			if (!GetDictionary().Contains(key))
			{
				BuildInstance(key, builder);
			}

			return (T) GetDictionary()[key];
		}

		public void ClearScopedInstance(string key)
		{
			if (GetDictionary().Contains(key))
			{
				ClearInstance(key);
			}
		}

		public void ClearInstance(string key)
		{
			lock (GetDictionary().SyncRoot)
			{
				if (GetDictionary().Contains(key))
				{
					RemoveInstance(key);
				}
			}
		}

		private void RemoveInstance(string key)
		{
			GetDictionary().Remove(key);
		}

		private void BuildInstance(string key, Func<T> builder)
		{
			lock (GetDictionary().SyncRoot)
			{
				if (!GetDictionary().Contains(key))
				{
					AddInstance(key, builder);
				}
			}
		}

		private void AddInstance(string key, Func<T> builder) {
			T instance = builder.Invoke();
			GetDictionary().Add(key, instance);
		}
	}
}