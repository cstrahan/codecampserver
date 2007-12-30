using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Rhino.Mocks;
using System.Web;
using NUnit.Framework;
using System.ComponentModel;

namespace CodeCampServer.UnitTests
{
    public static class TestHelper
    {
        public static void AssertRoute(RouteCollection routes, string url, object expectations)
        {
            MockRepository mocks = new MockRepository();
            IHttpContext httpContext;

            using (mocks.Record())
            {                
                httpContext = mocks.DynamicIHttpContext(url);
            }

            using (mocks.Playback())
            {
                RouteData routeData = routes.GetRouteData(httpContext);
                Assert.IsNotNull(routeData, "Should have found the route");

                foreach (PropertyValue property in GetProperties(expectations))
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
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
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
