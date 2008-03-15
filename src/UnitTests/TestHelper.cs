using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Routing;
using Rhino.Mocks;
using NUnit.Framework;

namespace CodeCampServer.UnitTests
{
    public static class TestHelper
    {
        public static void AssertRoute(RouteCollection routes, string url, object expectations)
        {
            var mocks = new MockRepository();
            HttpContextBase httpContext;

            using (mocks.Record())
            {
                httpContext = mocks.FakeHttpContext(url);
            }

            using (mocks.Playback())
            {
                var routeData = routes.GetRouteData(httpContext);
                Assert.IsNotNull(routeData, "Should have found the route");

                foreach (var property in GetProperties(expectations))
                {
                    Assert.IsTrue(string.Equals((string)property.Value
                      , (string)routeData.Values[property.Name]
                      , StringComparison.InvariantCultureIgnoreCase)
                      , string.Format("Did not expect '{0}' for '{1}'."
                        , property.Value, property.Name));
                }
            }
        }

        private static IEnumerable<PropertyValue> GetProperties(object o)
        {
            if (o != null)                
            {
                var props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue {Name = prop.Name, Value = val};
                    }
                }
            }
        }

        private sealed class PropertyValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }


}
