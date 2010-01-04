using MvcContrib.UI.InputBuilder.Conventions;

namespace UITestHelper
{
	public abstract class BaseFormInputManipulator<TInputBuilderConvention, TInputValue, TBrowserDriver> : IFormInputManipulator<TInputBuilderConvention, TBrowserDriver> 
		where TBrowserDriver : IBrowserDriver
		where TInputBuilderConvention : IPropertyViewModelFactory
	{
		public void SetInput(string name, object value, TBrowserDriver browserDriver)
		{
			var typedValue = (TInputValue) value;
			SetTypedInputValue(name, typedValue, browserDriver);
		}

		public abstract void SetTypedInputValue(string name, TInputValue value, TBrowserDriver browserDriver);
	}
}