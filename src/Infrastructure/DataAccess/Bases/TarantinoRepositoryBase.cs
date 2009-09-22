using System.Reflection;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate;

namespace Tarantino.Infrastructure.Commons.DataAccess.Repositories
{
    public class RepositoryBase
    {
        private readonly ISessionBuilder _sessionBuilder;

        public RepositoryBase(ISessionBuilder sessionFactory)
        {
            _sessionBuilder = sessionFactory;
        }

        public virtual string ConfigurationFile { get; set; }

        protected ISession GetSession()
        {
            var session = ConfigurationFile == null ? _sessionBuilder.GetSession() : _sessionBuilder.GetSession(ConfigurationFile);
            return session;
        }

    }
}