using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.UI.Binders.Entities
{
	public interface IEntityModelBinderFactory
	{
		IEntityModelBinder GetEntityModelBinder(Type entityType);
	}

	public class EntityModelBinderFactory : AbstractFactoryBase<IEntityModelBinder>, IEntityModelBinderFactory
	{
		public static Func<Type, IEntityModelBinder> GetDefault = modelBinderType => DefaultUnconfiguredState();

		public IEntityModelBinder GetEntityModelBinder(Type entityType)
		{
			var repositoryType = typeof (IRepository<>).MakeGenericType(entityType);
			var modelBinderType = typeof (ModelBinder<,>).MakeGenericType(entityType, repositoryType);

			return GetDefault(modelBinderType);
		}
	}
}