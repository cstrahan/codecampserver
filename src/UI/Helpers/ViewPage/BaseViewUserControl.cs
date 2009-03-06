using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CodeCampServer.DependencyResolution;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewUserControl : ViewUserControl, IViewBase
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly IDisplayErrorMessages _displayErrorMessages;

		public BaseViewUserControl()
		{
			_inputBuilderFactory = DependencyRegistrar.Resolve<IInputBuilderFactory>();
			_displayErrorMessages = DependencyRegistrar.Resolve<IDisplayErrorMessages>();
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

		public void PartialInputFor<TModel>(Expression<Func<TModel, object>> expression)
		{
			ViewBaseExtensions.PartialInputFor(this, expression);
		}
	}
}