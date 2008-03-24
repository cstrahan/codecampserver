using System.Web;

namespace CodeCampServer.UnitTests.Website
{
    public class HttpContextStub : HttpContextBase
    {
        private readonly HttpResponseBase _response;

        public HttpContextStub(HttpResponseBase response)
        {
            _response = response;
        }

        public HttpContextStub()
        {
        }

        public override HttpResponseBase Response
        {
            get { return _response; }
        }
    }
}