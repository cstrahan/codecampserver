using NHibernate;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public interface IUnitOfWork:Tarantino.RulesEngine.IUnitOfWork
	{
		ISession CurrentSession { get; }		
	}
}