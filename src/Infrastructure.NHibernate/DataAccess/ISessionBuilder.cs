using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public interface ISessionBuilder
	{
		ISession GetSession();
	}
}