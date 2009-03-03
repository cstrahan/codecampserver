using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputSpecification
	{
		string InputId { get; }
		PropertyInfo PropertyInfo { get; }
		object CustomAttributes { get; }
		HtmlHelper Helper { get; }
		bool UseSpanAsLabel { get; }
		string InputName { get; }
		int? InputIndex { get; }
		LambdaExpression Expression { get; }
		Type InputBuilderType { get; }
		object InputValue { get; }
	}
}