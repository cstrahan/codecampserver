using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Helpers.Binders
{
	public interface IKeyedModelBinder : IModelBinder
	{
	}

	public class KeyedModelBinder<TEntity, TRepository> : ModelBinder<TEntity, TRepository>, IKeyedModelBinder
		where TEntity : KeyedObject
		where TRepository :
			IKeyedRepository<TEntity>
	{
		public KeyedModelBinder(TRepository repository) : base(repository)
		{
		}

		public override ModelBinderResult BindModel(ModelBindingContext bindingContext)
		{
			try
			{
				ValueProviderResult value = GetRequestValue(bindingContext, bindingContext.ModelName);
				string attemptedValue = value.AttemptedValue;

				if (string.IsNullOrEmpty(attemptedValue)) return base.BindModel(bindingContext);

				TEntity match = _repository.GetByKey(attemptedValue);
				return new ModelBinderResult(match);
			}
			catch (Exception ex)
			{
				string message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                               bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
		}

		protected override ValueProviderResult GetRequestValue(ModelBindingContext bindingContext, string requestKey)
		{
			string key = requestKey;
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(key);
			if (value == null && !key.EndsWith("key"))
			{
				value = GetRequestValue(bindingContext, requestKey + "key");
			}

			return value;
		}
	}
}