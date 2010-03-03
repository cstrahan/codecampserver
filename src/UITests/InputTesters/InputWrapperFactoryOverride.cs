using MvcContrib.TestHelper.Ui;

namespace UITestHelper
{
    public class InputWrapperFactoryOverride : InputTesterFactory
    {
        public InputWrapperFactoryOverride()
        {
            Insert(0, new ScriptWrittenInputWrapperFactory());
            Insert(0, new DateTimeInputWrapperFactory());
        }
    }
}