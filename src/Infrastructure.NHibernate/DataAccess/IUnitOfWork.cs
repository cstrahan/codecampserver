using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public interface IUnitOfWork : Tarantino.RulesEngine.IUnitOfWork
	{
		void Begin();
		void Commit();
		void RollBack();
		ISession CurrentSession { get; }
	}
}