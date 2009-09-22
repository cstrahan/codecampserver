using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
    public class HybridSessionBuilder : ISessionBuilder
    {
        private static readonly Dictionary<string, ISessionFactory> _sessionFactories = new Dictionary<string, ISessionFactory>();
        private static readonly Dictionary<string, ISession> _currentSessions = new Dictionary<string, ISession>();

        private const string _defaultConfigFileName = "hibernate.cfg.xml";

        public ISessionFactory GetSessionFactory()
        {
            return GetSessionFactory(_defaultConfigFileName);
        }

        public ISession GetSession()
        {
            return GetSession(_defaultConfigFileName);
        }

        public virtual ISession GetSession(string configurationFile)
        {
            var factory = GetSessionFactory(configurationFile);
            var session = getExistingOrNewSession(factory, configurationFile);
            //Logger.Debug(this, string.Format("Using ISession {0}", session.GetHashCode()));
            return session;
        }

        public IStatelessSession GetStatelessSession()
        {
            return GetStatelessSession(_defaultConfigFileName);
        }

        public virtual IStatelessSession GetStatelessSession(string configurationFile)
        {
            var factory = GetSessionFactory(configurationFile);
            var session = factory.OpenStatelessSession();
            //Logger.Debug(this,string.Format("Using IStatelessSession {0}",session.GetHashCode()));
            return session;
        }

        public Configuration GetConfiguration()
        {
            return GetConfiguration(_defaultConfigFileName);
        }

        public virtual Configuration GetConfiguration(string configurationFile)
        {
            var configuration = new Configuration();
            configuration.Configure(GetFileName(configurationFile));
            return configuration;
        }

        public ISession GetExistingWebSession()
        {
            return GetExistingWebSession(_defaultConfigFileName);
        }

        public virtual ISession GetExistingWebSession(string configurationFile)
        {
            return HttpContext.Current.Items[configurationFile] as ISession;
        }

        public static void ResetSession()
        {
            new HybridSessionBuilder().GetSession().Dispose();
        }

        public static void ResetSession(string configurationFile)
        {
            new HybridSessionBuilder().GetSession(configurationFile).Dispose();
        }

        public virtual ISessionFactory GetSessionFactory(string configurationFile)
        {
            if (!_sessionFactories.ContainsKey(configurationFile))
            {
                var configuration = GetConfiguration(configurationFile);
                _sessionFactories[configurationFile] = configuration.BuildSessionFactory();
            }

            return _sessionFactories[configurationFile];
        }

        private ISession getExistingOrNewSession(ISessionFactory factory,
                                                 string configurationFile)
        {
            if (HttpContext.Current != null)
            {
                var session = GetExistingWebSession();

                if (session == null || !session.IsOpen)
                {
                    session = openSessionAndAddToContext(factory, configurationFile);
                }

                return session;
            }

            var currentSession = _currentSessions.ContainsKey(configurationFile)
                                     ? _currentSessions[configurationFile]
                                     : null;
            if (currentSession == null || !currentSession.IsOpen)
            {
                _currentSessions[configurationFile] = OpenSession(factory);
            }

            return _currentSessions[configurationFile];
        }

        protected virtual ISession OpenSession(ISessionFactory factory)
        {
            return factory.OpenSession();
        }

        private ISession openSessionAndAddToContext(ISessionFactory factory,
                                                    string configurationFile)
        {
            var session = OpenSession(factory);
            HttpContext.Current.Items.Remove(configurationFile);
            HttpContext.Current.Items.Add(configurationFile, session);
            return session;
        }

        private static string GetFileName(string file)
        {
            var fileName = file;

            var fileExists = File.Exists(file);

            if (!fileExists && HttpContext.Current != null)
            {
                var binPath =
                    Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "bin");
                fileName = Path.Combine(binPath, fileName);
            }

            if (!File.Exists(fileName))
            {
                var message =
                    string.Format("Could not locate NHibernate configuration file at: {0}",
                                  fileName);
                throw new ApplicationException(message);
            }

            return fileName;
        }
    }
}