using System;
using System.Linq;

namespace CodeCampServer.IntegrationTests
{
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