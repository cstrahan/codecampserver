using MbUnit.Framework;

namespace CodeCampServer.RegressionTests.Web.Smoke
{
	[TestFixture, Category("WatiN"), Category("Smoke")]
	[Description("Smoke Test - Verifies the user can navigate to each page")]
	public class SmokeNavigate : WebTest
	{
	}
}