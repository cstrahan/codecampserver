using System;
using NHibernate;
using Tarantino.RulesEngine;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class UnitOfWork:IUnitOfWork
	{
		private readonly ISessionBuilder _sessionBuilder;
		private ITransaction _transaction;

		public UnitOfWork(ISessionBuilder sessionBuilder)
		{
			_sessionBuilder = sessionBuilder;
		}

		public void Dispose()
		{
			if(_transaction!=null)
			{
				_transaction.Dispose();
			}
		}

		public void Begin()
		{
			_transaction = _sessionBuilder.GetSession().BeginTransaction();
		}

		public void RollBack()
		{
			_sessionBuilder.GetSession().Transaction.Commit();
		}

		public void Commit()
		{
			_sessionBuilder.GetSession().Transaction.Commit();
		}

		public ISession CurrentSession
		{
			get { throw new NotImplementedException(); }
		}
	}
}