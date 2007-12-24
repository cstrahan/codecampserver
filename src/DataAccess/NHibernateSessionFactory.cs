using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.DataAccess
{
    public static class NHibernateSessionFactory
    {
        private static ISessionFactory _factory;

        public static ISession GetSession()
        {
            return GetFactory().OpenSession();
        }

        private static ISessionFactory GetFactory()
        {
            if (_factory != null)
            {
                return _factory;
            }

            Configuration configuration = new Configuration();
            configuration.AddAssembly(Assembly.GetExecutingAssembly());
            ISessionFactory factory = configuration.BuildSessionFactory();
            _factory = factory;

            return factory;
        }
    }
}