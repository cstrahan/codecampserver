using System.Collections.Generic;

namespace CodeCampServer.Core.Common
{
	public static class QueryLimitExtensions
	{
		public const int ResultsLimit = 200;

		public static string GetSizeMessage<T>(this ICollection<T> collection)
		{
			var limitedMessage =  collection.WasLimitedQuery() ? " (limited)" : string.Empty;
			var rowsLabel = collection.Count != 1 ? "rows" : "row";
			return string.Format("{0} {1}{2}", collection.Count, rowsLabel, limitedMessage);
		}

		public static bool WasLimitedQuery<T>(this ICollection<T> collection)
		{
			return collection.Count == ResultsLimit;
		}
	}
}