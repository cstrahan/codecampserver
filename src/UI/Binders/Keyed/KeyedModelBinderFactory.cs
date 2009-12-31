using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;

namespace CodeCampServer.UI.Binders.Keyed
{
	public interface IKeyedModelBinderFactory
	{
		IKeyedModelBinder GetBinder(Type entityType);
	}

	public class KeyedModelBinderFactory : AbstractFactoryBase<IKeyedModelBinder>, IKeyedModelBinderFactory
	{
		public static Func<Type, IKeyedModelBinder> GetDefault = modelBinderType => DefaultUnconfiguredState();

		public IKeyedModelBinder GetBinder(Type entityType)
		{
			Type repositoryType = typeof (IKeyedRepository<>).MakeGenericType(entityType);
			Type modelBinderType = typeof (KeyedModelBinder<,>).MakeGenericType(entityType, repositoryType);
			return GetDefault(modelBinderType);
		}
	}
}