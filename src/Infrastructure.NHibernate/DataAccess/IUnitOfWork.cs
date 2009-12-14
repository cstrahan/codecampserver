using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public interface IUnitOfWork : Tarantino.RulesEngine.IUnitOfWork
	{
		ISession CurrentSession { get; }
	}
}