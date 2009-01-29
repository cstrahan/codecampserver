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
			LoopThroughAllTypesAndRegisterForOpenGenericsOfType(typeof (IKeyedRepository<>));
			LoopThroughAllTypesAndRegisterForOpenGenericsOfType(typeof (IRepository<>));
		}

		public void LoopThroughAllTypesAndRegisterForOpenGenericsOfType(Type openGenericInterface)
		{
			foreach (Type type in GetType().Assembly.GetTypes())
			{
				Type closedGenericInterface = ReflectionHelper.IsConcreteAssignableFromGeneric(type, openGenericInterface);

				if (closedGenericInterface != null)
					ForRequestedType(closedGenericInterface).AddInstance(new ConfiguredInstance(type));
			}
		}

		
	}

	public static class ReflectionHelper
	{
		public static Type IsConcreteAssignableFromGeneric(Type concreteType, Type openGenericInterfaceType)
		{
			Type closedGenericInterfaceWithoutParamerters = concreteType.GetInterfaces().Where(interfaceToTest =>
			{
				if (interfaceToTest.IsGenericType)
					return
						openGenericInterfaceType.MakeGenericType(
							interfaceToTest.GetGenericArguments()).IsAssignableFrom(interfaceToTest);
				return false;
			}
				).FirstOrDefault();

			if (closedGenericInterfaceWithoutParamerters != null)
			{
				Type closedGenericInterfaceWithParameters = openGenericInterfaceType.MakeGenericType(closedGenericInterfaceWithoutParamerters.GetGenericArguments());
				if (closedGenericInterfaceWithParameters.IsAssignableFrom(concreteType))
				{
					return closedGenericInterfaceWithParameters;
				}
			}
			return null;
		}
	}
}