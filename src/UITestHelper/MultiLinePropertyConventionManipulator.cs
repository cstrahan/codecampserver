using System;
using CodeCampServer.UI.InputBuilders;

namespace UITestHelper
{
	public class MultiLinePropertyConventionManipulator : BaseFormInputManipulator<MultiLinePropertyConvention, string, WatinDriver>
	{
		public MultiLinePropertyConventionManipulator() {}

		public override void SetTypedInputValue(string name, string value, WatinDriver browserDriver)
		{
			try
			{
				var ie = browserDriver.IE;
				ie.RunScript(("tinyMCE.execInstanceCommand('" + name + "', 'mceSetContent', false, '" + value + "')"));
			}
			catch (Exception)
			{
				browserDriver.CaptureScreenShot(browserDriver.GetTestname());
				throw;
			}
		}
	}
}