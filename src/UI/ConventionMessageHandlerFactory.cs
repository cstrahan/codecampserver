using System;
using MvcContrib.PortableAreas;

namespace CodeCampServer.UI
{
	public class ConventionMessageHandlerFactory : IMessageHandlerFactory
	{
		public static Func<Type, object> CreateDependencyCallback = (t) => (IMessageHandler) Activator.CreateInstance(t);

		public IMessageHandler Create(Type type)
		{
			return (IMessageHandler) CreateDependencyCallback(type);
		}
	}
}