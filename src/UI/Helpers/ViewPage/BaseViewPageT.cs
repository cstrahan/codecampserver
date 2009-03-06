using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Views;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewPage<TModel> : ViewPage<TModel>, IViewBase where TModel : class
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly IDisplayErrorMessages _displayErrorMessages;
		public new InputBuilderExtender<TModel> Html { get; private set; }

		public BaseViewPage()
		{
			_inputBuilderFactory = DependencyRegistrar.Resolve<IInputBuilderFactory>();
			_displayErrorMessages = DependencyRegistrar.Resolve<IDisplayErrorMessages>();
		}

		protected override void OnPreInit(EventArgs e)
		{
			Html = new InputBuilderExtender<TModel>(ViewContext, this, RouteTable.Routes);
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

		public void PartialInputFor(Expression<Func<TModel, object>> expression)
		{
			ViewBaseExtensions.PartialInputFor(this, expression);
		}
	}
}