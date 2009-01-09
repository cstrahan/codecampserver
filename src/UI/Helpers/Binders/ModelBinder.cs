using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.Binders
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

		public override ModelBinderResult BindModel(ModelBindingContext bindingContext)
		{
			try
			{
				ValueProviderResult value = GetRequestValue(bindingContext, bindingContext.ModelName);
				if (value == null) return new ModelBinderResult(default(TEntity));

				string attemptedValue = value.AttemptedValue;
				if (attemptedValue == "") return new ModelBinderResult(default(TEntity));
				
				var matchId = new Guid(attemptedValue);
				TEntity match = _repository.GetById(matchId);
				return new ModelBinderResult(match);
			}
			catch (Exception ex)
			{
				string message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                               bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
		}

		protected virtual ValueProviderResult GetRequestValue(ModelBindingContext bindingContext, string requestKey)
		{
			string key = requestKey;
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(key);
			if (value == null && !key.EndsWith("id"))
			{
				//try appending "id" on the key
				value = GetRequestValue(bindingContext, requestKey + "id");
			}

			return value;
		}
	}
}