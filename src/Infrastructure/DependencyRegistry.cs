using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace CodeCampServer.Infrastructure
{
	public class DependencyRegistry : Registry
	{
		protected override void configure()
		{
			LoopThroughAllTypesAndRegisterForTypeOf(typeof (IKeyedRepository<>));
			LoopThroughAllTypesAndRegisterForTypeOf(typeof (IRepository<>));
		}

		public void LoopThroughAllTypesAndRegisterForTypeOf(Type interfaceType)
		{
			foreach (Type type in GetType().Assembly.GetTypes())
			{
				Type specificInterfaceType = ReflectionHelper.IsConcreteAssignableFromGeneric(type, interfaceType);

				if (specificInterfaceType != null)
					ForRequestedType(specificInterfaceType).AddInstance(new ConfiguredInstance(type));
			}
		}

		
	}

	public static class ReflectionHelper
	{
		public static Type IsConcreteAssignableFromGeneric(Type concreteType, Type genericInterfaceType)
		{
			Type typeGenericInterface = concreteType.GetInterfaces().Where(i =>
			{
				if (i.IsGenericType)
					return
						genericInterfaceType.MakeGenericType(
							i.GetGenericArguments()).IsAssignableFrom(i);
				return false;
			}
				).FirstOrDefault();

			if (typeGenericInterface != null)
			{
				Type it = genericInterfaceType.MakeGenericType(typeGenericInterface.GetGenericArguments());
				if (it.IsAssignableFrom(concreteType))
				{
					return it;
				}
			}
			return null;
		}
	}
}