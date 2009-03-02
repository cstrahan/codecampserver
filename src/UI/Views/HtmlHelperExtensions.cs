using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CodeCampServer.UI.Helpers.ViewPage;

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
	}
}