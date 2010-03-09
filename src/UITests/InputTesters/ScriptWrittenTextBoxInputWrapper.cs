using System;
using MvcContrib.TestHelper.Ui;

namespace CodeCampServerUiTests.InputTesters
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