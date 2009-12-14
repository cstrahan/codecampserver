using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public interface ISessionSource
	{
		ISession CreateSession();
	}
}