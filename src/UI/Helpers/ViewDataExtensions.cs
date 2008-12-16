using System;
using System.Collections.Generic;
using MvcContrib;

namespace CodeCampServer.UI
{
	public static class ViewDataExtensions
	{
		public static IDictionary<string, object> Add(this IDictionary<string, object> bag, object anObject, Type objectType)
		{
			if (bag.ContainsKey(getKey(objectType)))
			{
				string message = string.Format("You can only add one default object for type '{0}'.", objectType);
				throw new ArgumentException(message);
			}

			bag.Add(getKey(objectType), anObject);
			return bag;
		}

		public static T GetOrDefault<T>(this IDictionary<string, object> bag)
		{
			var key = getKey(typeof (T));
			if (!bag.ContainsKey(key))
			{
				return default(T);
			}

			return bag.Get<T>();
		}

		private static string getKey(Type type)
		{
			return type.FullName;
		}

	}
}