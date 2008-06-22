using System.Web;

namespace CodeCampServer.UnitTests.Website
{
	public class HttpContextStub : HttpContextBase
	{
		private readonly HttpResponseBase _response;
		private readonly HttpRequestBase _request;

		public HttpContextStub(HttpRequestBase request)
		{
			_request = request;
		}

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

		public override HttpRequestBase Request
		{
			get { return _request; }
		}
	}
}