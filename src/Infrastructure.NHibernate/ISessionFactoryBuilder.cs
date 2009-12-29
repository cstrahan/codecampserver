using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public interface ISessionFactoryBuilder
	{
		ISessionFactory GetFactory();
	}
}