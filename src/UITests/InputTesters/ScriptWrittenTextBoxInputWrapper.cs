using System;
using System.Linq.Expressions;
using MvcContrib.TestHelper.Ui;
using MvcContrib.UI.InputBuilder.Helpers;

namespace UITestHelper
{
    public class ScriptWrittenTextBoxInputWrapper : InputTesterBase<string>
    {
        public ScriptWrittenTextBoxInputWrapper(string name, string value) : base(value, name) {}

        public override void AssertInputValueMatches(IBrowserDriver browserDriver)
        {
            throw new NotImplementedException();
        }

        public override void SetInput(IBrowserDriver browserDriver)
        {
            browserDriver.ExecuteScript(("tinyMCE.execInstanceCommand('" + _inputName + "', 'mceSetContent', false, '" +
                                         _value + "')"));
        }
    }
}