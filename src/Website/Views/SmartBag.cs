using System;
using System.Collections;

namespace CodeCampServer.Website.Views
{
	public class SmartBag : Hashtable
	{
		public SmartBag(params object[] initialObjects)
		{
			foreach (object obj in initialObjects)
			{
				Add(obj);
			}
		}

		public virtual T Get<T>()
		{
			return Get<T>(typeof (T));
		}

		public virtual object Get(Type type)
		{
			if (!ContainsKey(type))
			{
				string message = string.Format("No object exists that is of type '{0}'.", type);
				throw new ArgumentException(message);
			}

			return this[type];
		}

		/// <summary>
		/// Adds an object using the type as the dictionary key
		/// </summary>
		public virtual void Add(object anObject)
		{
			Type type = anObject.GetType();
			if (ContainsKey(type))
			{
				string message = string.Format("You can only add one default object for type '{0}'.", type);
				throw new ArgumentException(message);
			}

			Add(type, anObject);
		}

		public virtual bool Contains<T>()
		{
			return ContainsKey(typeof (T));
		}

		public virtual T Get<T>(object key)
		{
			if (!ContainsKey(key))
			{
				string message = string.Format("No object exists with key '{0}'.", key);
				throw new ArgumentException(message);
			}

			return (T) this[key];
		}

		public virtual int GetCount(Type type)
		{
			int count = 0;
			foreach (object value in Values)
			{
				if (type.Equals(value.GetType()))
				{
					count++;
				}
			}

			return count;
		}
	}
}