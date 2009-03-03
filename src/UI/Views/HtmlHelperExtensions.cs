using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CodeCampServer.UI.Helpers.ViewPage;
using CodeCampServer.UI.Helpers.ViewPage.InputBuilders;

namespace CodeCampServer.UI.Views
{
	public static class HtmlHelperExtensions
	{
		public static IInputSpecificationExpression Input<TModel>(this HtmlHelper<TModel> helper,
		                                                            Expression<Func<TModel, object>> expr)
			where TModel : class
		{
			var view = (IViewBase) helper.ViewDataContainer;
			return view.InputFor(expr);
		}

		public static IInputSpecificationExpression TextBox<TModel>(this HtmlHelper<TModel> helper,
																																Expression<Func<TModel, object>> expr)
			where TModel : class
		{
			var view = (IViewBase)helper.ViewDataContainer;
			return view.InputFor(expr).Using<TextBoxInputBuilder>();
		}
	}
}