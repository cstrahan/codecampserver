using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.DataAccess
{
    public class RepositoryBase
    {
        private static ISessionFactory _factory;

        protected static ISession getSession()
        {
            return getFactory().OpenSession();
        }

        private static ISessionFactory getFactory()
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