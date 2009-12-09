using NHibernate;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public interface ISessionSource
	{
		ISession CreateSession();
	}
}