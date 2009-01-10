using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputSpecification
	{
		string InputId { get; }
		bool Inline { get; }
		bool RenderLabel { get; }
		PropertyInfo PropertyInfo { get; }
		object CustomAttributes { get; }
		HtmlHelper Helper { get; }
		UrlHelper Url { get; }
		bool UseSpanAsLabel { get; }
		bool AttachCleaner { get; }
		bool InvalidOption { get; }
		string InputName { get; }
		int? InputIndex { get; }
		LambdaExpression Expression { get; }
	}
}