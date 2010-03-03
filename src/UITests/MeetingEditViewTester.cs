using System;
using System.Configuration;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Ui;
using MvcContrib.TestHelper.WatiN;
using MvcContrib.UI.InputBuilder.Conventions;
using MvcContrib.UI.InputBuilder.Helpers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using UITestHelper;
using WatiN.Core;

namespace CodeCampServerUiTests
{
    [TestFixture]
    public class MeetingEditViewTester
    {
        private IBrowserDriver _webBrowser;

        [TestFixtureSetUp]
        public void Setup()
        {
            string baseurl = ConfigurationManager.AppSettings["url"];

            _webBrowser = new WatinDriver(new IE(), baseurl);

            InputTesterFactory.Default = () => new InputWrapperFactoryOverride();
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

        private string GetUrl(string controller, string action)
        {
            new RouteConfigurator().RegisterRoutes(() => { });
            var builder = new TestControllerBuilder();
            var context = new RequestContext(builder.HttpContext, new RouteData());
            context.HttpContext.Response.Expect(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(
                new Func<string, string>(s => s)).Repeat.Any();
            var urlhelper = new UrlHelper(context);
            return urlhelper.RouteUrl("default", new {controller, action});
        }

        [Test]
        public void Should_create_a_new_meeting()
        {
            _webBrowser.ScreenCaptureOnFailure(() =>
               {
               Form<LoginInputProxy>("/login/login/index")
                   .Input(m => m.Username, "admin")
                   .Input(m => m.Password, "password")
                   .Submit();
                   _webBrowser.VerifyPage("home.index");                   
                   
               
                Form<MeetingInput>(GetUrl("meeting", "new"))
                   .Input(m => m.Name, "TX")
                   .Input(m => m.Topic, "my topic")
                   .Input(m => m.Summary, "this will be a normal meeting")
                   .Input(m => m.Description, "The description")
                   .Input(m => m.Key, Guid.NewGuid().ToString())
                   .Input(m => m.LocationName, "our location")
                   .Input(m => m.LocationUrl, "http://foolocation.com")
                   .Input(m => m.SpeakerBio, "this is a great speaker")
                   .Input(m => m.SpeakerName, "bart simpson")
                   .Input(m => m.SpeakerUrl, "http://thesimpsons.com")
                   .Input(m => m.TimeZone, "CST")
                   .Input(m => m.StartDate, "12/11/2010 12:00 pm")
                   .Input(m => m.EndDate, "12/11/2010 1:00 pm")
                   .Submit();
                   _webBrowser.VerifyPage("home.index");

               
           });
        }

        [Test]
        public void Should_require_fields()
        {
            _webBrowser.ScreenCaptureOnFailure(() =>
                                                   {
                                                       Form<LoginInputProxy>("/login/login/index")
                                                           .Input(m => m.Username, "admin")
                                                           .Input(m => m.Password, "password")
                                                           .Submit();
                                                       _webBrowser.VerifyPage("home.index");

                                                       Form<MeetingInput>("/Meeting/New")
                                                           .Input(m => m.Name, "foo")
                                                           .Submit();
                                                       _webBrowser.VerifyPage("meeting.edit");
                                                       
                                                       _webBrowser.ValidationSummaryExists();
                                                       _webBrowser.ValidationSummaryContainsMessageFor<MeetingInput>(
                                                           m => m.Topic);
                                                       _webBrowser.AssertValue<MeetingInput>(m => m.Name, "foo");
                                                   });
        }
    }

    public static class AssertExtensions
    {
        public static IBrowserDriver AssertValue<TFormType>(this IBrowserDriver driver,
                                                            Expression<Func<TFormType, object>> expression,
                                                            string expectedValue)
        {
            string id = ReflectionHelper.BuildNameFrom(expression);
            string value = driver.GetValue(id);
            value.ShouldBe(expectedValue);
            return driver;
        }

        public static IBrowserDriver ValidationSummaryContainsMessageFor<TModelType>(this IBrowserDriver driver,
                                                                                     Expression
                                                                                         <Func<TModelType, object>>
                                                                                         expression)
        {
            var localDriver = (WatinDriver) driver;
            string displayname = new DefaultNameConvention().PropertyName(ReflectionHelper.FindPropertyFromExpression(expression));
            var jquerySelector = string.Format(@"$('div.{0} ul li:contains(""{1}"")').text()", "validation-summary-errors", displayname);
            var count =(string)localDriver.EvaluateScript(jquerySelector);
            Assert.That(count.Contains(displayname));
            return driver;
        }

        public static IBrowserDriver ValidationSummaryExists(this IBrowserDriver driver)
        {
            var localDriver = (WatinDriver) driver;
            var count = (int) localDriver.EvaluateScript("$('div.validation-summary-errors').length");
            Assert.That(count, Is.EqualTo(1));
            return driver;
        }
        public static IBrowserDriver VerifyPage(this IBrowserDriver driver,string identifier)
        {
            var localDriver = (WatinDriver)driver;
            var value = (string)localDriver.EvaluateScript("$('input[name=controller-action]').val()");
            Assert.That(value.ToLower(),Is.EqualTo( identifier.ToLower()));
            return driver;
        }

        public static IBrowserDriver UrlShouldBe(this IBrowserDriver driver, string expectedValue)
        {
            Assert.That(driver.Url.ToLower(), Is.EqualTo(expectedValue.ToLower()));
            return driver;
        }

        public static string ShouldBe(this string actualValue, string expectedValue)
        {
            Assert.That(actualValue.ToLower(), Is.EqualTo(expectedValue.ToLower()));
            return actualValue;
        }
    }
}