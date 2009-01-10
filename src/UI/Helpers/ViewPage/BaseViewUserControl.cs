using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using StructureMap;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewUserControl : ViewUserControl, IViewBase
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly IDisplayErrorMessages _displayErrorMessages;

		public BaseViewUserControl()
		{
			_inputBuilderFactory = ObjectFactory.GetInstance<IInputBuilderFactory>();
			_displayErrorMessages = ObjectFactory.GetInstance<IDisplayErrorMessages>();
		}

		public IDisplayErrorMessages Errors
		{
			get
			{
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