using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.DataAccess
{
	public interface ISessionBuilder
	{
		ISession GetSession();
		Configuration GetConfiguration();
	}
}