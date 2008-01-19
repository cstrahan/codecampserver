using System;
using System.Collections;

namespace CodeCampServer.Website.Views
{
	public class SmartBag : Hashtable
	{
		public T Get<T>()
		{
			return (T)Get(typeof(T));
		}

		public object Get(Type type)
		{
			if (!ContainsKey(type))
			{
				string message = string.Format("No object exists that is of type '{0}'.", type);
				throw new ArgumentException(message);
			}

			return this[type];
		}

		public void Add(object anObject)
		{
			Type type = anObject.GetType();
			if (ContainsKey(type))
			{
				string message = string.Format("You can only add one default object for type '{0}'.", type);
				throw new ArgumentException(message);
			}

			Add(type, anObject);
		}

		public bool Contains<T>()
		{
			return ContainsKey(typeof (T));
		}
	}
}