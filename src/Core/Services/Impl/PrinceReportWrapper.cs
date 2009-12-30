using System.Web;

namespace CodeCampServer.Core.Services.Impl
{
    public interface IPrinceWrapper
    {
        void AttachPrinceFilter(HttpContextBase httpContextBase);
    }

    public class PrinceReportWrapper : IPrinceReportWrapper
    {
        private readonly IPrinceWrapper _princeWrapper;

        public PrinceReportWrapper(IPrinceWrapper princeWrapper)
        {
            _princeWrapper = princeWrapper;
        }

        public void AttachPrinceFilter(HttpContextBase httpContextBase)
        {
            _princeWrapper.AttachPrinceFilter(httpContextBase);
        }
    }
}