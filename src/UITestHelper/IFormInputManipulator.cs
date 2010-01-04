using MvcContrib.UI.InputBuilder.Conventions;

namespace UITestHelper
{
	public interface IFormInputManipulator<TInputBuilderConvention, TBrowserDriver>
		where TBrowserDriver : IBrowserDriver
		where TInputBuilderConvention : IPropertyViewModelFactory
	{
		void SetInput(string name, object value, TBrowserDriver browserDriver);
	}
}