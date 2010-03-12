using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI;
using CodeCampServerUiTests.InputTesters;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Ui;
using MvcContrib.TestHelper.WatiN;
using NUnit.Framework;
using Rhino.Mocks;
using WatiN.Core;

namespace CodeCampServerUiTests
{
	[TestFixture]
	public class UiTestBase 
	{
		protected IBrowserDriver _webBrowser;

		[TestFixtureSetUp]
		public  void FixtureSetup()
		{
			string baseurl = ConfigurationManager.AppSettings["url"];

			_webBrowser = new WatinDriver(new IE(), baseurl);

			InputTesterFactory.Default = () => new InputWrapperFactoryOverride();
			MultipleInputTesterFactory.Default = () => new MultipleInputFactoryOverride();

		}

		[SetUp]
		public  void Setup()
		{
			
		}


		[TestFixtureTearDown]
		public void teardown()
		{
			_webBrowser.Dispose();
			_webBrowser = null;
		}

		public InputForm<T> Form<T>(string url)
		{
			return new InputForm<T>(_webBrowser.Navigate(url));
		}

		protected string GetUrl(string controller, string action)
		{
			new RouteConfigurator().RegisterRoutes(() => { });
			var builder = new TestControllerBuilder();
			var context = new RequestContext(builder.HttpContext, new RouteData());
			context.HttpContext.Response.Expect(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(
				new Func<string, string>(s => s)).Repeat.Any();
			var urlhelper = new UrlHelper(context);
			return urlhelper.RouteUrl("default", new { controller, action });
		}
	}
}
