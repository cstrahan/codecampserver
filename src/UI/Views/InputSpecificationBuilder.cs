using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Views
{
	public class InputSpecificationBuilder
	{
		public IInputSpecificationExpression InputFor<TModel>(IViewBase baseView,
		                                                      Expression<Func<TModel, object>> expression)
		{
			return new InputSpecification(baseView.Html, baseView.InputBuilderFactory, baseView.Url, expression,
			                              GetParentExpression(baseView));
		}

		private static LambdaExpression GetParentExpression(IViewDataContainer baseView)
		{
			object parent;
			if (baseView.ViewData.TryGetValue("ParentExpression", out parent))
			{
				return (LambdaExpression)parent;
			}

			return null;
		}
	}
}