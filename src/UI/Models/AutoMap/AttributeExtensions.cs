using System;
using System.Linq;
using System.Reflection;

namespace CodeCampServer.UI.Models.AutoMap
{
	public static class AttributeExtensions
	{
		public static bool HasCustomAttribute<T>(this PropertyInfo p) where T : Attribute
		{
			return p.GetCustomAttributes(typeof(T), false).Length == 1;
		}

		public static T GetCustomAttribute<T>(this PropertyInfo p) where T : Attribute
		{
			return p.GetCustomAttributes(typeof(T), false).Cast<T>().FirstOrDefault();
		}

		public static T GetCustomAttribute<T>(this PropertyInfo p, bool inherit) where T : Attribute
		{
			return p.GetCustomAttributes(typeof(T), inherit).Cast<T>().FirstOrDefault();
		}
	}
}