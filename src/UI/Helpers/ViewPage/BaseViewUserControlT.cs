using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using StructureMap;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewUserControl<TModel> : ViewUserControl<TModel>, IViewBase where TModel : class
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly IDisplayErrorMessages _displayErrorMessages;

		public BaseViewUserControl()
		{
			_inputBuilderFactory = ObjectFactory.GetInstance<IInputBuilderFactory>();
			_displayErrorMessages = ObjectFactory.GetInstance<IDisplayErrorMessages>();
		}

		public IInputBuilderFactory InputBuilderFactory
		{
			get { return _inputBuilderFactory; }
		}

		public IDisplayErrorMessages Errors
		{
			get
			{
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

		public TModel Model
		{
			get { return ViewData.Model; }
		}
	}
}