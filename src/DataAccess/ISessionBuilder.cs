using NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace CodeCampServer.DataAccess
{
    [PluginFamily("Default")]
    public interface ISessionBuilder
    {
        ISession GetSession(Database selectedDatabase);
        Configuration GetConfiguration(Database selectedDatabase);
    }
}