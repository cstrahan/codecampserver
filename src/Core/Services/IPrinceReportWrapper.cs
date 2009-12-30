using System.Web;

namespace CodeCampServer.Core.Services
{
    public interface IPrinceReportWrapper 
    {
        void AttachPrinceFilter(HttpContextBase httpContextBase);
    }
}