using System.Linq.Expressions;
using System.Reflection;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.TestHelper.Ui;
using MvcContrib.UI.InputBuilder.Conventions;
using MvcContrib.UI.InputBuilder.Helpers;

namespace CodeCampServer.UITests.InputTesters
{
	public class ScriptWrittenInputWrapperFactory : IInputTesterFactory
	{
		public bool CanHandle(PropertyInfo propertyInfo)
		{
			return propertyInfo.AttributeExists<MultilineAttribute>();
		}

		public IInputTester Create(LambdaExpression expression, string text)
		{
			return new ScriptWrittenTextBoxInputWrapper(ReflectionHelper.BuildNameFrom(expression), text);
		}
	}
}