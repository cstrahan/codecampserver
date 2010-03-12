using MvcContrib.TestHelper.Ui;

namespace CodeCampServerUiTests.InputTesters
{
	public class InputWrapperFactoryOverride : InputTesterFactory
	{
		public InputWrapperFactoryOverride()
		{
			Insert(0, new ScriptWrittenInputWrapperFactory());
			Insert(0, new DateTimeInputWrapperFactory());
		}
	}

	public class MultipleInputFactoryOverride : MultipleInputTesterFactory
	{
		public MultipleInputFactoryOverride()
		{
			Insert(0, new SelectMultipleInputWrapperFactory());
		}
	}
}