using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Infrastructure.AutoMap;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class AutoMappedFilterAttribute : ActionFilterAttribute
	{
		private readonly Type _modelType;
		private readonly Type _dtoType;
		private readonly string _key;

		public AutoMappedFilterAttribute(Type modelType, Type dtoType)
			: this(modelType, dtoType, null)
		{
		}

		public AutoMappedFilterAttribute(Type modelType, Type dtoType, string key)
		{
			_modelType = modelType;
			_dtoType = dtoType;
			_key = key;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (_key != null && filterContext.Controller.ViewData.ContainsKey(_key))
			{
				object model = filterContext.Controller.ViewData[_key];
				object dto = AutoMapper.Map(model, ModelType, DtoType);

				filterContext.Controller.ViewData.Remove(_key);
				filterContext.Controller.ViewData[_key] = dto;
			}
			else if (filterContext.Controller.ViewData.Contains(ModelType))
			{
				object model = ((IDictionary<string, object>) filterContext.Controller.ViewData).Get(ModelType);
				object dto = AutoMapper.Map(model, ModelType, DtoType);

				filterContext.Controller.ViewData.Add(dto, DtoType);
			}
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
}