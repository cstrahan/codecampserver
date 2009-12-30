using System.Web;
using Infrastructure.Prince;

namespace CodeCampServer.Core
{
    public class PrinceReportWrapper
    {
        public void AttachPrinceFilter(HttpContext httpContext)
        {
            new PrinceWrapper().AttachPrinceFilter(httpContext);
        }
    }
}