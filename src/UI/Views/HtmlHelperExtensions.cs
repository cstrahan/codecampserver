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
			return GetInputSpec(helper, expr);
		}

		public static IInputSpecificationExpression Input<TModel>(this HtmlHelper<TModel> helper,
		                                                          Expression<Func<TModel, object>> expr,
		                                                          object value)
			where TModel : class
		{
			return GetInputSpec(helper, expr).WithValue(value);
		}

		private static IInputSpecificationExpression GetInputSpec<TModel>(HtmlHelper<TModel> helper,
		                                                                  Expression<Func<TModel, object>> expr)
			where TModel : class
		{
			var view = (IViewBase) helper.ViewDataContainer;
			return view.InputFor(expr);
		}

		public static IInputSpecificationExpression TextInput<TModel>(this HtmlHelper<TModel> helper,
		                                                              Expression<Func<TModel, object>> expr)
			where TModel : class
		{
			return GetInputSpec(helper, expr).Using<TextBoxInputBuilder>();
		}

		public static IInputSpecificationExpression HiddenInput<TModel>(this HtmlHelper<TModel> helper,
		                                                                Expression<Func<TModel, object>> expr)
			where TModel : class
		{
			return GetInputSpec(helper, expr).Using<HiddenInputBuilder>();
		}

		public static IInputSpecificationExpression Label<TModel>(this HtmlHelper<TModel> helper,
		                                                          Expression<Func<TModel, object>> expr)
			where TModel : class
		{
			return GetInputSpec(helper, expr).Using<NoInputBuilder>();
		}
	}
}