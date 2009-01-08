using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NBehave.Spec.NUnit
{
	public static class StopgapNBehaveExtensions
	{
		public static void ShouldContain<T>(this IEnumerable<T> collection, T shouldContain)
		{
			CollectionAssert.Contains(collection, shouldContain);
		}

		public static void ShouldNotContain<T>(this IEnumerable<T> collection, T shouldContain)
		{
			CollectionAssert.DoesNotContain(collection, shouldContain);
		}

		public static void ShouldBeOfLength<T>(this IEnumerable<T> collection, int length)
		{
			collection.ShouldNotBeNull();
			collection.Count().ShouldEqual(length);
		}
	}
}