using System;
using System.Linq.Expressions;
using System.Reflection;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.TestHelper.Ui;
using ReflectionHelper=MvcContrib.UI.InputBuilder.Helpers.ReflectionHelper;

namespace CodeCampServerUiTests.InputTesters
{
	public class SelectMultipleInputWrapperFactory : IMultipleInputTesterFactory
	{
		public bool CanHandle(PropertyInfo info)
		{
			return info.HasCustomAttribute<MultiSelectAttribute>();
		}

		public IInputTester Create(LambdaExpression expression, string[] text)
		{
			return new SelectMultipleInputWrapper(ReflectionHelper.BuildNameFrom(expression), text);
		}
	}

	public class SelectMultipleInputWrapper : IInputTester
	{
		private readonly string _baseName;
		private readonly string[] _text;

		public SelectMultipleInputWrapper(string baseName, string[] text)
		{
			_baseName = baseName;
			_text = text;
		}

		public void SetInput(IBrowserDriver browserDriver)
		{
			foreach (var selectText in _text)
			{
				browserDriver.SetValue(_baseName, selectText);
			}
			//  browserDriver.SetValue(_baseName+"_date",input.ToShortDateString()); - sets text field with string value
		}

		public void AssertInputValueMatches(IBrowserDriver browserDriver)
		{
			throw new NotImplementedException();
		}
	}
}