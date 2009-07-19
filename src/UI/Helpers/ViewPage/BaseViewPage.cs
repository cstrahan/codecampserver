using System;
using System.Linq.Expressions;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewPage : BaseViewPage<object>, IViewBase
	{		
		public void PartialInputFor<TModel>(Expression<Func<TModel, object>> expression)
		{
			ViewBaseExtensions.PartialInputFor(this, expression);
		}
	}
}