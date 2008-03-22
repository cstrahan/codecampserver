using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.DataAccess
{	
    public interface ISessionBuilder
    {
        ISession GetSession(Database selectedDatabase);
        Configuration GetConfiguration(Database selectedDatabase);
    }
}