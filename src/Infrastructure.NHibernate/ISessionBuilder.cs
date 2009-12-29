using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public interface ISessionBuilder
	{
		ISession GetSession();
	}
}