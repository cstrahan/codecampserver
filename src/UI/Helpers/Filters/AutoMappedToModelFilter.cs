using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.UI.Filters;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class AutoMappedToModelFilterAttribute : ActionFilterAttribute
	{
		private readonly Type _modelType;
		private readonly Type _dtoType;

		public AutoMappedToModelFilterAttribute(Type modelType, Type dtoType)
		{
			_modelType = modelType;
			_dtoType = dtoType;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var filter = new AutoMappedToModelFilter(ModelType, DtoType);

			filter.OnActionExecuted(filterContext);
		}

		public Type ModelType
		{
			get { return _modelType; }
		}

		public Type DtoType
		{
			get { return _dtoType; }
		}
	}

	public class AutoMappedToModelFilter : BaseActionFilter
	{
		private readonly Type _modelType;
		private readonly Type _dtoType;

		public AutoMappedToModelFilter(Type modelType, Type dtoType)
		{
			_modelType = modelType;
			_dtoType = dtoType;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			object dto = null;
			if (filterContext.Controller.ViewData.Contains(_modelType))
			{
				object model = ((IDictionary<string, object>) filterContext.Controller.ViewData).Get(_modelType);
				dto = Mapper.Map(model, _modelType, _dtoType);

				filterContext.Controller.ViewData.Add(dto, _dtoType);
			}

			filterContext.Controller.ViewData.Model = dto;
		}
	}
}