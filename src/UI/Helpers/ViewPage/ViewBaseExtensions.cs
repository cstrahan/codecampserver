using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Common;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public static class ViewBaseExtensions
	{
		private static LambdaExpression GetParentExpression(IViewDataContainer baseView)
		{
			object parent;
			if (baseView.ViewData.TryGetValue("ParentExpression", out parent))
			{
				return (LambdaExpression) parent;
			}

			return null;
		}

		public static IInputSpecificationExpression InputFor<TModel>(this IViewBase baseView,
		                                                             Expression<Func<TModel, object>> expression)
		{
			return new InputSpecification(baseView.Html, baseView.InputBuilderFactory, baseView.Url, expression,
			                              GetParentExpression(baseView));
		}

		public static void PartialInputFor<TModel>(this IViewBase baseView, Expression<Func<TModel, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.FindProperty(expression);
			object newModel = ExpressionHelper.Evaluate(expression, baseView.ViewData.Model);

			var newViewData = new ViewDataDictionary(baseView.ViewData) {Model = newModel};
			newViewData.Add("ParentExpression", expression);

			baseView.Html.RenderPartial(property.PropertyType.Name, newModel, newViewData);
		}
	}
}