using System;
using WatiN.Core;
using CodeCampServer.UI.InputBuilders;

namespace UITestHelper
{
	public class InputBuilderPropertyConventionManipulator : BaseFormInputManipulator<InputBuilderPropertyConvention, string, WatinDriver>
	{
		public InputBuilderPropertyConventionManipulator() {}

		public override void SetTypedInputValue(string name, string value, WatinDriver browserDriver)
		{
			try
			{
				TextField textField = browserDriver.IE.TextField(Find.ByName(name));
				textField.Value = value;
			}
			catch (Exception)
			{
				browserDriver.CaptureScreenShot(browserDriver.GetTestname());
				throw;
			}
			
		}
	}
}