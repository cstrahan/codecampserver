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

		public static IDictionary<string, object> ToDictionary(this object obj)
		{
			IDictionary<string, object> result = new Dictionary<string, object>();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			foreach (PropertyDescriptor property in properties)
			{
				result.Add(property.Name, property.GetValue(obj));
			}
			return result;
		}
	}
}