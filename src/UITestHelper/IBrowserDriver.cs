namespace UITestHelper
{
	public interface IBrowserDriver {
		void SetInput<TFormType>(InputWrapperBase<TFormType> wrapper);
		void ClickButton(string name);
	}
}