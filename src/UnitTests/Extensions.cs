using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests
{
    public static class Extensions
    {
        public static IHttpContext DynamicIHttpContext(this MockRepository mocks, string url)
        {
            IHttpContext context = mocks.DynamicMock<IHttpContext>();
            IHttpRequest request = mocks.DynamicMock<IHttpRequest>();
            IHttpResponse response = mocks.DynamicMock<IHttpResponse>();

            SetupResult.For(context.Request).Return(request);
            SetupResult.For(context.Response).Return(response);
            SetupResult.For(request.AppRelativeCurrentExecutionFilePath).Return(url);
            SetupResult.For(request.PathInfo).Return(string.Empty);            

            return context;
        }
    }
}
