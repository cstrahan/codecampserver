using System;
using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.ActionResults
{
	public class AutoMappedViewResult : ViewResult
	{
		public static Func<object, Type, Type, object> Map = (a, b, c) =>
		                                                     	{
		                                                     		throw new InvalidOperationException(
		                                                     			"The Mapping function must be set on the AutoMapperResult class");
		                                                     	};

		private  Type _viewModelType;

		public AutoMappedViewResult(Type type)
		{
			_viewModelType = type;
		}

		public Type ViewModelType
		{
			get { return _viewModelType; }
			set { _viewModelType = value; }
		}

		public override void ExecuteResult(ControllerContext context)
		{
			base.ViewData.Model = Map(ViewData.Model, ViewData.Model.GetType(), ViewModelType);
			base.ExecuteResult(context);
		}
	}
}