using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace CodeCampServer.Infrastructure
{
	public class StructureMapServiceLocator : IServiceLocator
	{
		public object GetService(Type serviceType)
		{
			return ObjectFactory.GetInstance(serviceType);
		}

		public object GetInstance(Type serviceType)
		{
			return ObjectFactory.GetInstance(serviceType);
		}

		public object GetInstance(Type serviceType, string key)
		{
			return ObjectFactory.GetNamedInstance(serviceType, key);
		}

		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			IList instances = ObjectFactory.GetAllInstances(serviceType);
			var objects = new List<object>();
			foreach (object o in instances)
			{
				objects.Add(o);
			}
			return objects;
		}

		public TService GetInstance<TService>()
		{
			return ObjectFactory.GetInstance<TService>();
		}

		public TService GetInstance<TService>(string key)
		{
			return ObjectFactory.GetNamedInstance<TService>(key);
		}

		public IEnumerable<TService> GetAllInstances<TService>()
		{
			return ObjectFactory.GetAllInstances<TService>();
		}
	}
}