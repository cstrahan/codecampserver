namespace UITestHelper
{
	public interface IInputWrapper
	{
		void SetInput(IBrowserDriver browserDriver);
		void AssertInputValueMatches(IBrowserDriver browserDriver);
	}
}