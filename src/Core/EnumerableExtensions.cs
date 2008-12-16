using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<TResult> Distinct<TSource, TResult>(this IEnumerable<TSource> source,
		                                                              Func<TSource, TResult> comparer)
		{
			return source.Distinct(new DynamicComparer<TSource, TResult>(comparer)).Select(comparer);
		}

		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> action)
		{
			int i = 0;
			foreach (var item in items)
			{
				action(item, i++);
			}
		}

		public class DynamicComparer<T, TResult> : IEqualityComparer<T>
		{
			private readonly Func<T, TResult> _selector;

			public DynamicComparer(Func<T, TResult> selector)
			{
				_selector = selector;
			}

			public bool Equals(T x, T y)
			{
				if (Equals(x, null) && Equals(y, null))
					return true;

				if (Equals(x, null) || Equals(y, null))
					return false;

				TResult result1 = _selector(x);
				TResult result2 = _selector(y);

				if (Equals(result1, null) && Equals(result2, null))
					return true;

				if (Equals(result1, null) || Equals(result2, null))
					return false;

				return result1.Equals(result2);
			}

			public int GetHashCode(T obj)
			{
				if (Equals(obj, null))
					return 0;

				TResult result = _selector(obj);

				if (Equals(result, null))
					return 0;

				return result.GetHashCode();
			}
		}
	}
}