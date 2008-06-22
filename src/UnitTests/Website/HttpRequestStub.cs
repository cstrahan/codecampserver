using System;
using System.Web;

namespace CodeCampServer.UnitTests.Website
{
	public class HttpRequestStub : HttpRequestBase
	{
		private readonly Uri _url;

		public HttpRequestStub(Uri url)
		{
			_url = url;
		}

		public override Uri Url
		{
			get { return _url; }
		}
	}
}