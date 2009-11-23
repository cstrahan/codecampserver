namespace UITestHelper
{
	public interface IBrowserDriver {
		void SetInput(IInputWrapper wrapper);
		void ClickButton(string name);
	}
}