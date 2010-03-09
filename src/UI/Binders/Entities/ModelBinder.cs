using System;
using System.Web.Mvc;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.UI.Binders.Entities
{
	public interface IEntityModelBinder
	{
		BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
	}

	public class ModelBinder<TEntity, TRepository> : IEntityModelBinder where TRepository : IRepository<TEntity>
	                                                                    where TEntity : PersistentObject
	{
		protected readonly TRepository _repository;

		public ModelBinder(TRepository repository)
		{
			_repository = repository;
		}

		public virtual BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			try
			{
				var value = GetRequestValue(bindingContext, bindingContext.ModelName, controllerContext);
				if (value == null) return new BindResult(null, null);

				var attemptedValue = value.AttemptedValue;
				if (attemptedValue == "") return new BindResult(null, null);

				var matchId = new Guid(attemptedValue);
				var match = _repository.GetById(matchId);

				return new BindResult(match, value);
			}
			catch (Exception ex)
			{
				var message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                            bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
		}

		protected virtual ValueProviderResult GetRequestValue(ModelBindingContext bindingContext, string requestKey,
		                                                      ControllerContext controllerContext)
		{
			var key = requestKey;
			var valueProvider = bindingContext.ValueProvider.GetValue(requestKey);
			if (valueProvider == null && !key.EndsWith(GetOptionalSuffix()))
			{
				//try appending "id" on the key
				valueProvider = GetRequestValue(bindingContext, requestKey + GetOptionalSuffix(), controllerContext);
			}

			return valueProvider;
		}

		protected virtual string GetOptionalSuffix()
		{
			return "id";
		}
	}
}