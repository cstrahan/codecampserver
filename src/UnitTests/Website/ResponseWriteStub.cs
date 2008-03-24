using System.Web;

namespace CodeCampServer.UnitTests.Website
{
    public class ResponseWriteStub : HttpResponseBase
    {
        public string ActualWritten;

        public override void Write(string s)
        {
            ActualWritten = s;
        }
    }
}