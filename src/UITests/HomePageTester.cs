using System;
using NUnit.Framework;
using WatiN.Core;

namespace UITests
{
	
	[TestFixture]
	public class HomePageTester
	{
		[STAThread]
		[Test]
		public void The_home_page_should_render()
		{
			var baseurl = System.Configuration.ConfigurationManager.AppSettings["url"];
			using (var ie = new IE(baseurl))
			{
				ie.Link(Find.ByText("all events")).Click();
				ie.Link(Find.ByText("about us")).Click();
				ie.Link(Find.ByText("Log in")).Click();
				ie.TextField("Username").TypeText("admin");
				ie.TextField("Password").TypeText("password");
				ie.Forms[0].Submit();
				ie.Link(Find.ByText("My Profile")).Click();
				ie.Link(Find.ByText("admin")).Click();
				ie.Link(Find.ByText("Edit User Groups")).Click();
				ie.Link(Find.ByText("admin")).Click();
				ie.Link(Find.ByText("Edit Users")).Click();
				ie.Link(Find.ByText("admin")).Click();
				ie.Link(Find.ByText("Edit Sponsors")).Click();
				ie.Link(Find.ByText("Log out")).Click();
			}
		}
	}
}