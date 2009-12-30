using System;
using CodeCampServer.Core.Services;

namespace CodeCampServer.Core
{
    public class PrinceReportWrapperFactory : AbstractFactoryBase<IPrinceReportWrapper>
    {
        public static Func<IPrinceReportWrapper> GetDefault = DefaultUnconfiguredState;
    }
}