using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodeCampServer.Core
{
	public static class ObjectExtensions
	{
		public static object GetPropertyValue(this object obj, string property)
		{
			return TypeDescriptor.GetProperties(obj)[property].GetValue(obj);
		}

		public static IDictionary ToDictionary(this object obj)
		{
			IDictionary result = new Dictionary<string, object>();
			var properties = TypeDescriptor.GetProperties(obj);
			foreach (PropertyDescriptor property in properties)
			{
				result.Add(property.Name, property.GetValue(obj));
			}
			return result;
		}
	}
}