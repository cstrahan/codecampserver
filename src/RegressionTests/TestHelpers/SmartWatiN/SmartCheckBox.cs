using WatiN.Core;

namespace RegressionTests.Web
{
    public class SmartCheckBox
    {
        private readonly CheckBox _checkBox;

        public SmartCheckBox(CheckBox checkBox)
        {
            _checkBox = checkBox;
        }

        public void Set(bool value)
        {
            _checkBox.Checked = value;
        }
    }
}