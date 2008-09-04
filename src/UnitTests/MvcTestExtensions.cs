using System.Web.Mvc;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests
{
    public static class MvcTestExtensions
    {
        public static void ShouldRenderDefaultView(this ActionResult result)
        {
            ShouldRenderView(result, string.Empty);            
        }

        public static void ShouldRenderView(this ActionResult result, string viewName)
        {
            var actualViewName = AssertResult<ViewResult>(result).ViewName;
            Assert.That(string.Compare(actualViewName, viewName, true), Is.EqualTo(0),
                string.Format("Expected {0} but was {1}", actualViewName, viewName));
        }

        public static IRedirectOptions ShouldRedirectTo(this ActionResult result, string action)
        {
            var routeResult = AssertResult<RedirectToRouteResult>(result);

            var options = new RedirectOptions(routeResult);            
            options.WithValue("Action", action);
            return options;
        }
      
        public static IRedirectOptions ShouldRedirectTo(this ActionResult result, string controller, string action)
        {
            var routeResult = AssertResult<RedirectToRouteResult>(result);
            
            var options = new RedirectOptions(routeResult);
            options.WithValue("Controller", controller);
            options.WithValue("Action", action);
            return options;
        }

        private static T AssertResult<T>(ActionResult result) where T : ActionResult
        {
            var actualResult = result as T;
            if (actualResult == null)
                Assert.Fail(string.Format("Expected {0} but was {1}", typeof (T).Name, result.GetType().Name));

            return actualResult;
        }
    }

    public class RedirectOptions : IRedirectOptions
    {
        private readonly RedirectToRouteResult _result;

        public RedirectOptions(RedirectToRouteResult result)
        {
            _result = result;
        }

        public void WithValue(string key, string expectedValue)
        {
            Assert.That(_result.Values.ContainsKey(key), "RouteValues didn't have key " + key);

            string actualValue = _result.Values[key].ToString();
            Assert.That(string.Compare(actualValue, expectedValue, true), Is.EqualTo(0), 
                string.Format("Expected {0} but was {1}", expectedValue, actualValue));
        }
    }

    public interface IRedirectOptions
    {
        void WithValue(string key, string expectedValue);
    }
}