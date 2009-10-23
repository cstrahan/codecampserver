using System;
using CodeCampServer.Infrastructure.DataAccess.Bases;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ISessionBuilder _sessionSource;
		private ITransaction _transaction;
		private bool _begun;
		private bool _disposed;
		private bool _rolledBack;

		public UnitOfWork(ISessionBuilder sessionSource)
		{
			_sessionSource = sessionSource;
		}

		public void Begin()
		{
			CheckIsDisposed();

			CurrentSession = _sessionSource.GetSession();
			
			//.CreateSession();

			BeginNewTransaction();
			_begun = true;
		}

		public void Commit()
		{
			CheckIsDisposed();
			CheckHasBegun();

			if (_transaction.IsActive && !_rolledBack)
			{
				Logger.Debug(this, string.Format("Commit transaction {0}", _transaction.GetHashCode()));
				_transaction.Commit();
			}

			BeginNewTransaction();
		}

		public void RollBack()
		{
			CheckIsDisposed();
			CheckHasBegun();

			if (_transaction.IsActive)
			{
				Logger.Debug(this, string.Format("Rollback transaction {0}", _transaction.GetHashCode()));
				_transaction.Rollback();
				_rolledBack = true;
			}

			BeginNewTransaction();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public ISession CurrentSession
		{
			get; private set;
		}

		private void BeginNewTransaction()
		{
			if (_transaction != null)
			{
				Logger.Debug(this, string.Format("Dispose transaction {0}", _transaction.GetHashCode()));
				_transaction.Dispose();
			}

			_transaction = CurrentSession.BeginTransaction();
			Logger.Debug(this, string.Format("Begin transaction {0}", _transaction.GetHashCode()));
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_begun || _disposed)
				return;

			if (disposing)
			{
				Logger.Debug(this, string.Format("Dispose transaction {0}", _transaction.GetHashCode()));
				_transaction.Dispose();
				CurrentSession.Dispose();
				CurrentSession = null;
			}

			_disposed = true;
		}

		private void CheckHasBegun()
		{
			if (!_begun)
				throw new InvalidOperationException("Must call Begin() on the unit of work before committing");
		}

		private void CheckIsDisposed()
		{
			if (_disposed) 
				throw new ObjectDisposedException(GetType().Name);
		}

	}
}