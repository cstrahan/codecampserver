using System.Collections.Generic;
using System.Web;
using CodeCampServer.DataAccess;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
    [Pluggable("Default")]
    public class HybridSessionBuilder : ISessionBuilder
    {
        private readonly IDictionary<Database, ISessionFactory> _sessionFactories = new Dictionary<Database, ISessionFactory>();
        private static ISession _currentSession;

        public ISession GetSession(Database selectedDatabase)
        {
            ISessionFactory factory = getSessionFactory(selectedDatabase);
            return getExistingOrNewSession(factory);
        }

        private ISessionFactory getSessionFactory(Database selectedDatabase)
        {
            if(!_sessionFactories.ContainsKey(selectedDatabase))
            {
                Configuration configuration = GetConfiguration(selectedDatabase);
                _sessionFactories.Add(selectedDatabase, configuration.BuildSessionFactory());
            }

            return _sessionFactories[selectedDatabase];
        }

        Configuration ISessionBuilder.GetConfiguration(Database selectedDatabase)
        {
            Configuration configuration = GetConfiguration(selectedDatabase);
            return configuration;
        }

        private static Configuration GetConfiguration(Database selectedDatabase)
        {
            string configFile = string.Format("hibernate-{0}.cfg.xml", selectedDatabase.ToString().ToLower());
            Configuration configuration = new Configuration();
            configuration.Configure(configFile);
            return configuration;
        }

        private ISession getExistingOrNewSession(ISessionFactory factory)
        {
            if(HttpContext.Current != null)
            {
                ISession session = GetExistingWebSession();
                if (session == null)
                {
                    session = openSessionAndAddToContext(factory);
                }
                else if(!session.IsOpen)
                {
                    session = openSessionAndAddToContext(factory);
                }

                return session;
            }

            if(_currentSession == null)
            {
                _currentSession = factory.OpenSession();
            }
            else if(!_currentSession.IsOpen)
            {
                _currentSession = factory.OpenSession();
            }

            return _currentSession;
        }

        public ISession GetExistingWebSession()
        {
            return (ISession) HttpContext.Current.Items[GetType().FullName] as ISession;
        }

        private ISession openSessionAndAddToContext(ISessionFactory factory)
        {
            ISession session = factory.OpenSession();
            HttpContext.Current.Items.Remove(GetType().FullName);
            HttpContext.Current.Items.Add(GetType().FullName, session);
            return session;
        }

        public static void ResetSession(Database selectedDatabase)
        {
            HybridSessionBuilder builder = new HybridSessionBuilder();
            builder.GetSession(selectedDatabase).Dispose();
        }
    }
}