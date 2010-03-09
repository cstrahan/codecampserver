using System;
using System.Linq.Expressions;
using System.Reflection;
using MvcContrib.TestHelper.Ui;
using MvcContrib.UI.InputBuilder.Helpers;

namespace CodeCampServerUiTests.InputTesters
{
	public class DateTimePickerInputWrapper : IInputTester
	{
		private readonly string _baseName;
		private readonly string _text;

		public DateTimePickerInputWrapper(string baseName, string text)
		{
			_baseName = baseName;
			_text = text;
		}

		public void SetInput(IBrowserDriver browserDriver)
		{
			var input = DateTime.Parse(_text);
			browserDriver.SetValue(_baseName+"_date",input.ToShortDateString());
			if (input.Hour == 0)
				browserDriver.SetValue(_baseName + "_hour", "12");
			else if (input.Hour > 12)
				browserDriver.SetValue(_baseName + "_hour", (input.Hour-12).ToString());
			else
				browserDriver.SetValue(_baseName + "_hour", input.Hour.ToString());

			browserDriver.SetValue(_baseName + "_minute", input.Minute.ToString());
			browserDriver.SetValue(_baseName + "_noon", input.Hour>=12?"P.M.":"A.M.");
			browserDriver.ExecuteScript("$('#" + _baseName + "_noon"+ "').blur();");
		}

		public void AssertInputValueMatches(IBrowserDriver browserDriver)
		{
			throw new NotImplementedException();
		}
	}

	public class DateTimeInputWrapperFactory : IInputTesterFactory
	{
		public bool CanHandle(PropertyInfo info)
		{
			return info.PropertyType == typeof(DateTime);
		}

		public IInputTester Create(LambdaExpression expression, string text)
		{
			return new DateTimePickerInputWrapper(ReflectionHelper.BuildNameFrom(expression), text);
		}
	}
}