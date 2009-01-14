using System;
using System.Linq.Expressions;
using StructureMap;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewPage : System.Web.Mvc.ViewPage, IViewBase
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly IDisplayErrorMessages _displayErrorMessages;

		public BaseViewPage()
		{
			_inputBuilderFactory = ObjectFactory.GetInstance<IInputBuilderFactory>();
			_displayErrorMessages = ObjectFactory.GetInstance<IDisplayErrorMessages>();
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

		public IInputBuilderFactory InputBuilderFactory
		{
			get { return _inputBuilderFactory; }
		}

		public IInputSpecificationExpression InputFor<TModel>(Expression<Func<TModel, object>> expression)
		{
			return ViewBaseExtensions.InputFor(this, expression);
		}

		public void PartialInputFor<TModel>(Expression<Func<TModel, object>> expression)
		{
			ViewBaseExtensions.PartialInputFor(this, expression);
		}
	}
}