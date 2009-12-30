using System;
using System.Web.Mvc;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain;

namespace CodeCampServer.UI.Binders
{
	public class ModelBinder<TEntity, TRepository> : DefaultModelBinder
		where TRepository : IRepository<TEntity>
		where TEntity : PersistentObject
	{
		protected readonly TRepository _repository;

		public ModelBinder(TRepository repository)
		{
			_repository = repository;
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			try
			{
				ValueProviderResult value = GetRequestValue(bindingContext, bindingContext.ModelName, controllerContext);
				if (value == null) return default(TEntity);

				string attemptedValue = value.AttemptedValue;
				if (attemptedValue == "") return default(TEntity);

				var matchId = new Guid(attemptedValue);
				TEntity match = _repository.GetById(matchId);
				return match;
			}
			catch (Exception ex)
			{
				string message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                               bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
		}

		protected virtual ValueProviderResult GetRequestValue(ModelBindingContext bindingContext, string requestKey, ControllerContext controllerContext)
		{
			string key = requestKey;
			ValueProviderResult valueProvider = bindingContext.ValueProvider.GetValue(controllerContext, requestKey);
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