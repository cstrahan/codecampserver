using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Binders.Entities;

namespace CodeCampServer.UI.Binders.Keyed
{
	public class KeyedModelBinder<TEntity, TRepository> : ModelBinder<TEntity, TRepository>, IKeyedModelBinder
		where TEntity : KeyedObject
		where TRepository :
			IKeyedRepository<TEntity>
	{
		public KeyedModelBinder(TRepository repository) : base(repository) {}

		public override BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			try
			{
				ValueProviderResult value = GetRequestValue(bindingContext, bindingContext.ModelName, controllerContext);


				if (value == null || string.IsNullOrEmpty(value.AttemptedValue))
					return base.BindModel(controllerContext, bindingContext);

				TEntity match = _repository.GetByKey(value.AttemptedValue);
				if (match != null)
					return new BindResult(match, value);
				else
					return base.BindModel(controllerContext, bindingContext);
			}
			catch (Exception ex)
			{
				string message = string.Format("Unable to locate a valid value for query string parameter '{0}'",
				                               bindingContext.ModelName);
				throw new ApplicationException(message, ex);
			}
		}

		protected override string GetOptionalSuffix()
		{
			return "key";
		}
	}

	public interface IKeyedModelBinder
	{
		BindResult BindModel(ControllerContext context, ModelBindingContext bindingContext);
	}
}