using System;
using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;

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
			Type repositoryType = typeof (IRepository<>).MakeGenericType(entityType);
			Type modelBinderType = typeof (ModelBinder<,>).MakeGenericType(entityType, repositoryType);

			return GetDefault(modelBinderType);
		}
	}
}