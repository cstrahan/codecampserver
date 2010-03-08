using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Interfaces;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class StructureMapServiceLocator : IServiceLocator, IDependencyResolver
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
			var instances = ObjectFactory.GetAllInstances(serviceType);
			var objects = new List<object>();
			foreach (var o in instances)
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

		public Interface GetImplementationOf<Interface>()
		{
			return ObjectFactory.GetInstance<Interface>();
		}

		public Interface GetImplementationOf<Interface>(Type type)
		{
			return (Interface) ObjectFactory.GetInstance(type);
		}

		public object GetImplementationOf(Type type)
		{
			return ObjectFactory.GetInstance(type);
		}

		public void DisposeImplementation(object instance) {}
	}
}