using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    public class DatabaseTesterBase : RepositoryBase
    {
        protected static ISessionBuilder _sessionBuilder = new HybridSessionBuilder();

        public DatabaseTesterBase()
            : base(_sessionBuilder)
        {
        }
    }
}