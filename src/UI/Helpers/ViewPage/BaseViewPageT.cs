using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CodeCampServer.DependencyResolution;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewPage<TModel> : ViewPage<TModel>, IViewBase where TModel : class
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly IDisplayErrorMessages _displayErrorMessages;

		public BaseViewPage()
		{
			_inputBuilderFactory = DependencyRegistrar.Resolve<IInputBuilderFactory>();
			_displayErrorMessages = DependencyRegistrar.Resolve<IDisplayErrorMessages>();
		}

		public IInputBuilderFactory InputBuilderFactory
		{
			get { return _inputBuilderFactory; }
		}

		public IDisplayErrorMessages Errors
		{
			get
			{
				_displayErrorMessages.TempData = TempData;
				_displayErrorMessages.ModelState = ViewData.ModelState;
				return _displayErrorMessages;
			}
		}

		public IInputSpecificationExpression InputFor(Expression<Func<TModel, object>> expression)
		{
			return ViewBaseExtensions.InputFor(this, expression);
		}

		public void PartialInputFor(Expression<Func<TModel, object>> expression)
		{
			ViewBaseExtensions.PartialInputFor(this, expression);
		}
	}
}