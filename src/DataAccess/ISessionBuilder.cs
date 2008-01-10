using CodeCampServer.Model;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace CodeCampServer.DataAccess
{
	[PluginFamily(Keys.DEFAULT)]
    public interface ISessionBuilder
    {
        ISession GetSession(Database selectedDatabase);
        Configuration GetConfiguration(Database selectedDatabase);
    }
}