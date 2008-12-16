using WatiN.Core;

namespace RegressionTests.Web
{
    public class SmartTextField
    {
        private readonly TextField _textField;

        public SmartTextField(TextField textField)
        {
            _textField = textField;
        }

        public void Set(string value)
        {
            _textField.Value = value;
        }
    }
}