using System;
using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ISessionBuilder _sessionBuilder;

		public UnitOfWork(ISessionBuilder sessionBuilder)
		{
			_sessionBuilder = sessionBuilder;
		}

		public void Begin()
		{
			BeginNewTransaction();
		}

		public void Commit()
		{
			CheckHasBegun();

			ITransaction transaction = GetTransaction();
			if (transaction.IsActive)
			{
				transaction.Commit();
			}
		}

		private ITransaction GetTransaction()
		{
			return GetSession().Transaction;
		}

		public void RollBack()
		{
			CheckHasBegun();

			if (GetTransaction().IsActive)
			{
				GetTransaction().Rollback();
			}
		}

		public ISession GetSession()
		{
			return _sessionBuilder.GetSession();
		}

		private void BeginNewTransaction()
		{
			if (GetTransaction().IsActive || GetTransaction().WasCommitted || GetTransaction().WasRolledBack)
			{
				GetTransaction().Dispose();
			}

			GetSession().BeginTransaction();
		}

		private void CheckHasBegun()
		{
			if (!GetTransaction().IsActive)
				throw new InvalidOperationException("Must call Begin() on the unit of work before committing");
		}

		public void Dispose()
		{
			GetSession().Dispose();
		}

		public void Invalidate()
		{
			RollBack();
		}
	}
}